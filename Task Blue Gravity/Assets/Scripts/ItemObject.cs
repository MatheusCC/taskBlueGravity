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
    private string itemName;
    [SerializeField]
    private Sprite itemSprite = null;
    [SerializeField]
    private float itemPrice = 0f;
    [SerializeField]
    private bool isEquipped = false;

    public string ItemName
    {
        get { return itemName; }
    }
    public Sprite ItemSprite
    { 
        get { return itemSprite; } 
    }
    public float ItemPrice
    {
        get { return itemPrice; }
    }

    public ItemID ItemEnum
    {
        get { return itemID; }
    }

    public bool IsEquipped
    {
        get { return isEquipped; }
        set { isEquipped = value; }
    }
}
