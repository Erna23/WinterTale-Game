using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class MiniGame : MonoBehaviour
{
    public Image MiniGameUI;
    public Text touchText;
    public Text totalCountText;
    public UnityEngine.UI.Slider progressBar;
    public Image item_image1;
    public Image item_image2;
    public GameObject Finish;
    public UnityEngine.UI.Button btnMiniGame;
    public Text Item_text;
    private DatabaseManager theDatabase;

    private int totalTouchCount;
    private int currentTouchCount;
    private int selectedItem;
    private bool gameCompleted = false;

    private Animator miniGameUIAnimator;
    private bool isShaking = false;
    private FadeManager theFade;
    
    void Start(){
        theDatabase = FindObjectOfType<DatabaseManager>();
        selectedItem = PlayerPrefs.GetInt("SelectedItem", 0);
        SetTouchCount();
        btnMiniGame.onClick.AddListener(OnConfirm);
        currentTouchCount = 0;
        LoadItemImage();
        UpdateUI();

        miniGameUIAnimator = MiniGameUI.GetComponent<Animator>();
        theFade = FindObjectOfType<FadeManager>();
        theFade.FadeIn();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && !isShaking) 
        {
            OnTouch();
            ShakeUI();
        }
        if (Input.GetMouseButtonUp(0) && isShaking)
        {
            StopShakeUI();
        }
    }

    void SetTouchCount() {
        // 아이템에 따라 터치 횟수 설정
        switch(selectedItem) {
            case 10001:
                totalTouchCount = 5;  // 아이템 1은 5번 터치
                break;
            case 20001:
                totalTouchCount = 10; // 아이템 2는 10번 터치
                break;
            case 30001:
                totalTouchCount = 7;  // 아이템 3은 7번 터치
                break;
            case 40001:
                totalTouchCount = 12; // 아이템 4는 15번 터치
                break;
        }
    }

    void LoadItemImage(){
        Sprite itemSprite = Resources.Load("ItemIcon/" + selectedItem.ToString(), typeof(Sprite)) as Sprite;
        item_image1.sprite = itemSprite;
        item_image2.sprite = itemSprite;
    }

    public void OnTouch(){
        if (gameCompleted){
            return;
        }
        currentTouchCount++;
        // 진행률 업데이트
        UpdateUI();
        // 게임 클리어 체크
        if (currentTouchCount >= totalTouchCount){
            gameCompleted = true;
            EffectSound.instance.Play(4);
            OnGameCompleted();
        }
    }

    // 흔들기 애니메이션 실행
    private void ShakeUI()
    {
        if (gameCompleted){
            return;
        }
        isShaking = true;
        EffectSound.instance.Play(3);
        miniGameUIAnimator.SetBool("IsShaking", true);  // 흔들림 상태 시작
    }

    // 흔들기 애니메이션 멈추기
    private void StopShakeUI()
    {
        isShaking = false;
        miniGameUIAnimator.SetBool("IsShaking", false);  // 흔들림 상태 종료
    }

    // 게임 완료 시 호출되는 함수
    private void OnGameCompleted(){
        Bag.instance.Get_Item(selectedItem);
        CanvasGroup canvasGroup = MiniGameUI.GetComponent<CanvasGroup>();
        if (canvasGroup == null){
            canvasGroup = MiniGameUI.gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0.1f;
        Finish.SetActive(true);
        Item_text.text = theDatabase.GetName(selectedItem) + "을 획득하였습니다!";
    }

    // UI 업데이트 함수
    private void UpdateUI(){
        totalCountText.text = $"{totalTouchCount}";
        touchText.text = $"{currentTouchCount}";
        float progress = (float)currentTouchCount / totalTouchCount;
        progressBar.value = progress;
        if (progress == 1){
            touchText.color = Color.green;
        }
        else{
            touchText.color = Color.white;
        }
    }

    // 확인 버튼 클릭 시 메인 화면으로 이동
    public void OnConfirm(){
        TransferScene transferScene = FindObjectOfType<TransferScene>();
        if (transferScene != null) {
            EffectSound.instance.Play(1);
            transferScene.Trans_PreviousScene(); // 이전 씬으로 돌아가기 호출
            theFade.FadeOut();
        }
    }
}
