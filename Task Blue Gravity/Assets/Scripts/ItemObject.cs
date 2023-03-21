using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "ShopItem/Item")]
public class ItemObject : ScriptableObject
{
    [SerializeField] 
    private string itemName;
    [SerializeField]
    private Sprite itemSprite = null;
    [SerializeField]
    private float itemPrice = 0f;


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
}
