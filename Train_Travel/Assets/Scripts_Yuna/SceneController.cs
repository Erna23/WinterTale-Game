using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject SceneButton;
    public GameObject YesNoButton;
    public GameObject LikeImage;
    private static readonly string[] allKeys =
    {
        "SelectedItem", "BagItemCount", "Item_0", "Item_1", "Item_2", "Item_3",
        "Item_Use_10001", "Item_Use_20001", "Item_Use_30001", "Item_Use_40001",
        "GameScene6_js", "GameScene6_ej", "GameScene6_sh", "GameScene6_shn", "GameScene6",
        "Travel", "NPC1_Favor", "NPC2_Favor", "NPC3_Favor", "NPC4_Favor", "DayCount"
    };
    public void ShowYesNoButton()
    {
        YesNoButton.SetActive(true);
    }
    public void HideYesNoButton()
    {
        YesNoButton.SetActive(false);
    }
    public void ShowSceneButton()
    {
        SceneButton.SetActive(true);
    }

    public void LikeImages()
    {
        LikeImage.SetActive(true);
    }
    public void LikeImages2()
    {
        LikeImage.SetActive(false);
    }

    public void LoadStartScene()
    {
        ResetGameData();
        SceneManager.LoadScene("Start");
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
    public void LoadNextScene1()
    {
        SceneManager.LoadScene("GameScene1");
        BGM.instance.Stop();
    }
    public void LoadNextScene2()
    {
        SceneManager.LoadScene("GameScene2");
        BGM.instance.Stop();
    }
    public void LoadNextScene3()
    {
        SceneManager.LoadScene("GameScene3");
        BGM.instance.Stop();
    }
    public void LoadNextScene4()
    {
        SceneManager.LoadScene("GameScene4");
        BGM.instance.Stop();
    }
    public void LoadNextScene5()
    {
        SceneManager.LoadScene("GameScene5");
        BGM.instance.Stop();
    }
    public void LoadNextScene6()
    {
        SceneManager.LoadScene("GameScene6");
        BGM.instance.Stop();
    }

    public void LoadTravelScene()
    {
        string currentScene = PlayerPrefs.GetString("Travel", "Korea");
        SceneManager.LoadScene(currentScene);
    }

    public void CheckEnding()
    {
        int sh_likes = PlayerPrefs.GetInt("NPC1_Favor", 0);
        int js_likes = PlayerPrefs.GetInt("NPC2_Favor", 0);
        int ej_likes = PlayerPrefs.GetInt("NPC3_Favor", 0);
        int shn_likes = PlayerPrefs.GetInt("NPC4_Favor", 0);

        List<int> likesList = new List<int> { sh_likes, js_likes, ej_likes, shn_likes };
        int maxLikes = Mathf.Max(likesList.ToArray());

        if (sh_likes < 50 && js_likes < 50 && ej_likes < 50 && shn_likes < 50)
        {
            SetEnding("GameScene6");
        }
        else
        {
            if (sh_likes == maxLikes && sh_likes > 0)
            {
                SetEnding("GameScene6_sh");
            }
            else if (js_likes == maxLikes && js_likes > 0)
            {
                SetEnding("GameScene6_js");
            }
            else if (ej_likes == maxLikes && ej_likes > 0)
            {
                SetEnding("GameScene6_ej");
            }
            else if (shn_likes == maxLikes && shn_likes > 0)
            {
                SetEnding("GameScene6_shn");
            }
            else
            {
                Debug.LogWarning("No valid ending was found!");
            }
        }
    }

    void SetEnding(string endingSceneName)
    {
        PlayerPrefs.SetInt(endingSceneName, 1);
        PlayerPrefs.Save();
        LoadEndingScene(endingSceneName);
    }

    void LoadEndingScene(string endingSceneName)
    {
        SceneManager.LoadScene(endingSceneName);
    }
}