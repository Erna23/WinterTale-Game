using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public class StoryPopup : MonoBehaviour
{
    public GameObject popup; 
    public Button[] actionButtons;
    public string sceneToLoad; 

    void Start()
    {
        popup.SetActive(false);

        foreach (Button button in actionButtons)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    public void ShowPopup()
    {
        popup.SetActive(true);
    }

    public void HidePopup()
    {
        popup.SetActive(false);
    }

   private void OnButtonClick()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void SetPopupAndScene(string sceneName)
    {
        ShowPopup();
        sceneToLoad = sceneName;
    }
}
