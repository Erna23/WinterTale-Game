using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferScene : MonoBehaviour
{
    public FadeManager theFade;
    public string Map;
    private static Stack<string> previousScenes = new Stack<string>(); // 스택 초기화

    void Start()
    {
        theFade = FindObjectOfType<FadeManager>();
        theFade.FadeIn();
        // 현재 씬 이름 저장
        string curScene = SceneManager.GetActiveScene().name;
        // 스택이 비어 있거나 마지막 씬이 현재 씬이 아닐 때 추가
        if (previousScenes.Count == 0 || previousScenes.Peek() != curScene) {
            previousScenes.Push(curScene);
        }
    }

    public void Trans_Scene() {
        StartCoroutine(TransferCoroutine(Map));
    }

    public void Trans_PreviousScene() {
        if (previousScenes.Count > 1)
        {
            previousScenes.Pop();
            string previousScene = previousScenes.Peek();
            StartCoroutine(TransferCoroutine(previousScene));
        }
    }

    public void Trans_Main(){
        string travel = PlayerPrefs.GetString("Travel", "Korea");
        int day_count = PlayerPrefs.GetInt("DayCount", 0);
        day_count--;
        PlayerPrefs.SetInt("DayCount", day_count);
        PlayerPrefs.Save();
        StartCoroutine(TransferCoroutine(travel));
    }

    IEnumerator TransferCoroutine(string sceneName) {
        EffectSound.instance.Play(0);
        theFade.FadeOut();
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(sceneName);
    }
}