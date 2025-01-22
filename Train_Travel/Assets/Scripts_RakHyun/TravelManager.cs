using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TravelManager : MonoBehaviour
{
    public Button[] travelButtons;  // 6개의 여행지 버튼 배열
    public string[] travelDestinations; // 각 여행지에 해당하는 씬이나 오브젝트
    private FadeManager theFade;

    private int currentUnlockedDestination = 0;  // 현재 활성화된 여행지 (0부터 시작)
    private static readonly string[] allKeys =
    {
        "SelectedItem", "BagItemCount", "Item_0", "Item_1", "Item_2", "Item_3",
        "Item_Use_10001", "Item_Use_20001", "Item_Use_30001", "Item_Use_40001",
        "GameScene6_js", "GameScene6_ej", "GameScene6_sh", "GameScene6_shn", "GameScene6",
        "Travel", "NPC1_Favor", "NPC2_Favor", "NPC3_Favor", "NPC4_Favor", "DayCount"
    };

    void Start()
    {
        CheckUnlockDestinations();
        theFade = FindObjectOfType<FadeManager>();
        BGM.instance.Play(0);
    }

    // 엔딩을 얻었을 때 호출
    public void OnEndingAchieved()
    {
        CheckUnlockDestinations();
    }

    // 버튼 상태 업데이트
    private void UpdateButtonStates()
    {
        for (int i = 0; i < travelButtons.Length; i++)
        {
            if (i <= currentUnlockedDestination)
            {
                // 버튼을 클릭 가능하게 하고 밝게 설정
                travelButtons[i].interactable = true;
                ColorBlock colors = travelButtons[i].colors;
                colors.normalColor = Color.white;  // 밝은 색으로 변경
                travelButtons[i].colors = colors;
            }
            else
            {
                // 버튼을 클릭 불가능하게 하고 어둡게 설정
                travelButtons[i].interactable = false;
                ColorBlock colors = travelButtons[i].colors;
                colors.normalColor = Color.gray;  // 어두운 색으로 변경
                travelButtons[i].colors = colors;
            }
        }
    }

    // 여행지 버튼을 클릭했을 때 해당 여행지로 이동하는 함수
    public void OnTravelButtonClicked(int index)
    {
        EffectSound.instance.Play(0);
        ResetGameData();    //data 초기화
        if (index <= currentUnlockedDestination)
        {
            // 여행지 씬으로 전환 (예: 씬 로딩)
            string sceneName = travelDestinations[index];
            PlayerPrefs.SetString("Travel", sceneName);
            StartCoroutine(TransferCoroutine(sceneName));
        }
    }

    public void ResetGameData()
    {
        ResetPlayerDataExceptEndings();
        PlayerPrefs.Save();
    }
    private void ResetPlayerDataExceptEndings()
    {
        foreach (var key in allKeys)
        {
            // "GameScene6"으로 시작하는 키는 제외하고 삭제
            if (!key.StartsWith("GameScene6"))
            {
                PlayerPrefs.DeleteKey(key);
            }
        }
    }

    IEnumerator TransferCoroutine(string sceneName) {
        theFade.FadeOut();
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(sceneName);
    }

    private void CheckUnlockDestinations()
    {
        int count = IsAnyEndingAchieved();
        if(count < 5){
            currentUnlockedDestination = count;
        }
        else if(count >= 5){
            currentUnlockedDestination = 6;
        }

        // 버튼 상태를 다시 업데이트
        UpdateButtonStates();
    }

    private int IsAnyEndingAchieved()
    {
        int count = 0;
        if(PlayerPrefs.GetInt("GameScene6", 0) == 1){
            Debug.LogWarning("GameScene6");
            count++;
        }
        if (PlayerPrefs.GetInt("GameScene6_js", 0) == 1)
        {
            Debug.LogWarning("GameScene6_js");
            count++;
        }
        if (PlayerPrefs.GetInt("GameScene6_ej", 0) == 1)
        {
            Debug.LogWarning("GameScene6_ej");
            count++;
        }
        if (PlayerPrefs.GetInt("GameScene6_sh", 0) == 1)
        {
            Debug.LogWarning("GameScene6_sh");
            count++;
        }
        if (PlayerPrefs.GetInt("GameScene6_shn", 0) == 1)
        {
            Debug.LogWarning("GameScene6_shn");
            count++;
        }
        return count;
    }
}