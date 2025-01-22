using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OkOrCancel : MonoBehaviour
{
    private Bag theBag;
    private BagSlot theSlot;
    public GameObject OOC;
    private Item selectedItem;
    public Text NPC;

    public void OK(){
        EffectSound.instance.Play(0);
        theBag = FindObjectOfType<Bag>();
        theSlot = FindObjectOfType<BagSlot>();
        theBag.UseItem(selectedItem);
        theSlot.Remove_Item();
        theBag.Show_Item();
        OOC.SetActive(false);
    }

    public void Cancel(){
        EffectSound.instance.Play(0);
        OOC.SetActive(false);
    }

    public void Set_Item(Item item)
    {
        selectedItem = item;
        if(selectedItem.item_ID == 10001){
            NPC.text = "<차승호>";
        }
        else if(selectedItem.item_ID == 20001){
            NPC.text = "<윤지수>";
        }
        else if(selectedItem.item_ID == 30001){
            NPC.text = "<한은지>";
        }
        else if(selectedItem.item_ID == 40001){
            NPC.text = "<김성훈>";
        }
    }
}