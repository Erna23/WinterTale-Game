using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Init : MonoBehaviour
{
    public Button btnInit;

    private static readonly string[] allKeys = 
    {
        "SelectedItem", "BagItemCount", "Item_0", "Item_1", "Item_2", "Item_3", 
        "Item_Use_10001", "Item_Use_20001", "Item_Use_30001", "Item_Use_40001", 
        "GameScene6_js", "GameScene6_ej", "GameScene6_sh", "GameScene6_shn", "GameScene6",
        "Travel", "NPC1_Favor", "NPC2_Favor", "NPC3_Favor", "NPC4_Favor"
    };

    void Start(){
        btnInit.onClick.AddListener(OnClick);
    }

    public void OnClick(){
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void ResetGameData()
    {
        ResetPlayerDataExceptEndings();
        PlayerPrefs.Save();
    }

    private void ResetPlayerDataExceptEndings()
    {
        // 모든 PlayerPrefs를 가져오고 엔딩 관련 키만 제외한 키를 삭제
        foreach (var key in allKeys)
        {
            // "GameScene6"으로 시작하는 키는 제외하고 삭제
            if (!key.StartsWith("GameScene6"))
            {
                PlayerPrefs.DeleteKey(key);
            }
        }
    }
}
