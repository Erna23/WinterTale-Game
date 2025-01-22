using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour
{
    public Button itemButton1;
    public Button itemButton2;
    public Button itemButton3;
    public Button itemButton4;
    public FadeManager theFade;

    void Start() {
        itemButton1.onClick.AddListener(() => SelectItem(10001));
        itemButton2.onClick.AddListener(() => SelectItem(20001));
        itemButton3.onClick.AddListener(() => SelectItem(30001));
        itemButton4.onClick.AddListener(() => SelectItem(40001));
        theFade = FindObjectOfType<FadeManager>();
    }

    void SelectItem(int itemId) {
        EffectSound.instance.Play(2);
        StartCoroutine(SelectItemCoroutine(itemId));
    }

    IEnumerator SelectItemCoroutine(int itemId){
        PlayerPrefs.SetInt("SelectedItem", itemId);
        PlayerPrefs.Save();
        theFade.FadeOut(0.1f);
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("MiniGame_Main");
    }
}
