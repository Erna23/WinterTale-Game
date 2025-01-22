using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int item_ID;
    public string item_Name;
    public int item_Count;
    public Sprite item_Icon;
    public ItemType item_Type;

    public enum ItemType{
        USE, ETC
    }

    public Item(int id, string name, ItemType type, int cnt  = 1){
        item_ID = id;
        item_Name = name;
        item_Count = cnt;
        item_Type = type;
        item_Icon = Resources.Load("ItemIcon/" + item_ID.ToString(), typeof(Sprite)) as Sprite;
    }
}
