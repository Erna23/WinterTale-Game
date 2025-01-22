using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class BagSlot : MonoBehaviour
{
    public Image Icon;
    private OkOrCancel theOOC;
    public Item item;

    void Start(){
        theOOC = FindObjectOfType<OkOrCancel>();
    }

    public void Add_Item(Item item){
        this.item = item;
        Icon.sprite = item.item_Icon;
    }

    public void Remove_Item(){
        Icon.sprite = null;
    }

    public void Selected(){
        EffectSound.instance.Play(0);
        theOOC.Set_Item(item);
        theOOC.OOC.SetActive(true);
    }
}
