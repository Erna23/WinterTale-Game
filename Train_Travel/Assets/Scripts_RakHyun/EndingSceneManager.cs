using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingSceneManager : MonoBehaviour
{
    public Button ending1;
    public Button ending2;
    public Button ending3;
    public Button ending4;
    public Button ending5;

    public Image ending1_Image;
    public Image ending2_Image;
    public Image ending3_Image;
    public Image ending4_Image;
    public Image ending5_Image;

    private FadeManager theFade;

    void Start()
    {
        UpdateButton();
        ending1.onClick.AddListener(() => LoadEndingScene("GameScene6_js"));
        ending2.onClick.AddListener(() => LoadEndingScene("GameScene6_sh"));
        ending3.onClick.AddListener(() => LoadEndingScene("GameScene6_ej"));
        ending4.onClick.AddListener(() => LoadEndingScene("GameScene6_shn"));
        ending5.onClick.AddListener(() => LoadEndingScene("GameScene6"));
        theFade = FindObjectOfType<FadeManager>();
    }

    void UpdateButton(){
        SetButton(ending1, ending1_Image, "GameScene6_js");
        SetButton(ending2, ending2_Image, "GameScene6_sh");
        SetButton(ending3, ending3_Image, "GameScene6_ej");
        SetButton(ending4, ending4_Image, "GameScene6_shn");
        SetButton(ending5, ending5_Image, "GameScene6");
    }

    void SetButton(Button btn, Image btnImage, string endingKey){
        int endingState = PlayerPrefs.GetInt(endingKey, 0);
        if(endingState == 1){
            btn.interactable = true;
            btnImage.color = Color.white;
        }
        else{
            btn.interactable = false;
            btnImage.color = Color.black;
        }
    }

    void LoadEndingScene(string Map){
        EffectSound.instance.Play(6);
        StartCoroutine(LoadEndingCoroutine(Map));
    }

    IEnumerator LoadEndingCoroutine(string Map){
        theFade.FadeOut();
        BGM.instance.Stop();
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(Map);
    }
}
