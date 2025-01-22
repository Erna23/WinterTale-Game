using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;
    public List<Item> item_List = new List<Item>();

    private void Awake() {
        if(instance == null){
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }
    }

    public void Item_Use(int id){
        int useCount = PlayerPrefs.GetInt("Item_" + id, 0);
        if (useCount >= 2) {
            Debug.Log("이미 2번 이상 선물했습니다");
            return;
        }
        useCount++;
        PlayerPrefs.SetInt("Item_" + id, useCount);
        PlayerPrefs.Save();
        switch(id){
            case 10001:
                Debug.Log("아이템1 사용-sh 호감도 상승");
                IncreaseFavor("NPC1", 5); // NPC1의 호감도를 5 증가시킴
                break;
            case 20001:
                Debug.Log("아이템2 사용-js 호감도 상승");
                IncreaseFavor("NPC2", 5); //NPC1의 호감도를 5 증가시킴
                break;
            case 30001:
                Debug.Log("아이템3 사용-ej 호감도 상승");
                IncreaseFavor("NPC3", 5); //NPC1의 호감도를 5 증가시킴
                break;
            case 40001:
                Debug.Log("아이템4 사용-shn 호감도 상승");
                IncreaseFavor("NPC4", 5); //NPC1의 호감도를 5 증가시킴
                break;
        }
    }

    void IncreaseFavor(string npc, int amount){
        int currentFavor = PlayerPrefs.GetInt(npc + "_Favor", 0);  //기존 호감도 값 가져오기
        currentFavor += amount;  //호감도 증가
        PlayerPrefs.SetInt(npc + "_Favor", currentFavor);  //업데이트된 호감도 저장
        PlayerPrefs.Save();
        Debug.Log(npc + "의 호감도가 " + amount + "만큼 증가하여 " + currentFavor + "이 되었습니다.");
    }

    void Start()
    {
        item_List.Add(new Item(10001, "아이템1", Item.ItemType.USE));
        item_List.Add(new Item(20001, "아이템2", Item.ItemType.USE));
        item_List.Add(new Item(30001, "아이템3", Item.ItemType.USE));
        item_List.Add(new Item(40001, "아이템4", Item.ItemType.USE));   
    }

    public string GetName(int id){
        for(int i = 0; i < item_List.Count; i++){
            if(id == item_List[i].item_ID){
                return item_List[i].item_Name;
            }
        }
        return "";
    }
}
