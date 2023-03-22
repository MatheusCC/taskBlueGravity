using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "ShopItem/Item")]
public class ItemObject : ScriptableObject
{   
    public enum ItemID
    {
        HELMET,
        BODY,
        WEAPON,
        SHIELD
    }

    [SerializeField]
    private ItemID itemID = ItemID.HELMET;
    [SerializeField] 
    private string nameItem;
    [SerializeField]
    private Sprite sprite = null;
    [SerializeField]
    private int price = 0;

    public string Name
    {
        get { return nameItem; }
    }
    public Sprite Sprite
    { 
        get { return sprite; } 
    }
    public int Price
    {
        get { return price; }
    }

    public ItemID ItemEnum
    {
        get { return itemID; }
    }
}
