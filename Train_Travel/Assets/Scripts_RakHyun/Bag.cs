using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class Bag : MonoBehaviour
{
    public static Bag instance;
    private DatabaseManager theDataBase;
    //가방의 슬롯
    private BagSlot[] slots;
    //플레이어가 소유한 아이템 리스트
    private List<Item> BagItemList;
    //슬롯의 부모 객체
    public Transform tf;
    //가방 활성화 체크
    private bool activated;
    //아이템 선택 시 다른 아이템 선택 방지
    private bool input;
    public GameObject floating_Text;
    
    void Start(){
        instance = this;
        theDataBase = FindObjectOfType<DatabaseManager>();
        BagItemList = new List<Item>();
        slots = tf.GetComponentsInChildren<BagSlot>();
        LoadBagItems();
    }

    void Update() {
        if(!input){
            activated = !activated;
            if(activated){
                Show_Item();
                input = true;

            }
            else{
                
            }
        }
    }

    public void Remove_Slot(){
        for(int i = 0; i < slots.Length; i++){
            slots[i].Remove_Item();
            slots[i].gameObject.SetActive(false);
        }
    }

    public void Show_Item(){
        Remove_Slot();
        for(int i = 0; i < BagItemList.Count; i++){
            slots[i].gameObject.SetActive(true);
            slots[i].Add_Item(BagItemList[i]);
        }
    }

    //미니게임 클리어 시 선택된 아이템 획득 함수
    public void Get_Item(int id, int cnt = 1){
        for (int i = 0; i < theDataBase.item_List.Count; i++) {
            if (id == theDataBase.item_List[i].item_ID) {
                BagItemList.Add(theDataBase.item_List[i]);
                SaveBagItems();
                Debug.Log(id + " 획득");
                return;
            }
        }
        Debug.LogError("데이터베이스에 해당 아이템이 존재하지 않습니다");
    }

    public void UseItem(Item item){
        if (item != null)
        {
            if(PlayerPrefs.GetInt("Item_" + item.item_ID) == 2){
                var _clone = Instantiate(floating_Text, new Vector3(0,0,0), Quaternion.Euler(Vector3.zero));
                _clone.GetComponent<FloatingText>().text.text = "이미 2번 이상 선물했습니다";
                _clone.transform.SetParent(this.transform);
                return;
            }
            theDataBase.Item_Use(item.item_ID);
            EffectSound.instance.Play(7);
            var clone = Instantiate(floating_Text, new Vector3(0,0,0), Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingText>().text.text = item.item_Name + " 사용";
            clone.transform.SetParent(this.transform);
            BagItemList.Remove(item);
            SaveBagItems();
            Show_Item();
        }
    }

    public void SaveBagItems() {
        PlayerPrefs.SetInt("BagItemCount", BagItemList.Count);

        for (int i = 0; i < BagItemList.Count; i++) {
            PlayerPrefs.SetInt("Item_" + i, BagItemList[i].item_ID);
        }
        PlayerPrefs.Save();
    }

    public void LoadBagItems() {
        int itemCount = PlayerPrefs.GetInt("BagItemCount", 0);
        BagItemList.Clear();
        for (int i = 0; i < itemCount; i++) {
            int itemID = PlayerPrefs.GetInt("Item_" + i);
            for (int j = 0; j < theDataBase.item_List.Count; j++) {
                if (theDataBase.item_List[j].item_ID == itemID) {
                    BagItemList.Add(theDataBase.item_List[j]);
                    break;
                }
            }
        }
        Show_Item();
    }
}
