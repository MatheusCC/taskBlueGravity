using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    private struct Equipments
    {
        public ItemObject.ItemID itemID;
        public SpriteRenderer spriteRender;
        public Sprite defaultSprite;
        public Item item;
    }

    /*
    [Header("Player Default Sprites")]
    [SerializeField]
    private Sprite body = null;
    [SerializeField]
    private Sprite helmet = null;
    [SerializeField]
    private Sprite shield = null;
    [SerializeField]
    private Sprite weapon = null;
    */

    [SerializeField]
    private Equipments[] playerEquipments = null;

    [SerializeField]
    private List<Item> playerInventoryItens = new List<Item>();

    public List<Item> PlayerInventoryItens
    {
        get { return playerInventoryItens; }
    }

    private void Update()
    {
        // With the click of the mouse left button
        // create a raycast down from the mouse position and detect if the shop was clicked;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);

            if (hit2D.collider != null)
            {
                //Open the shop if the shop as clicked
                if (hit2D.collider.gameObject.CompareTag("Merchant"))
                {
                    MenuManager.Instance.OpenShop();
                }
            }
        }
    }

    public void EquipItem(Item itemToEquip)
    {
        // Before to equip a new item,
        // check if has a item already equipped and unequip it
        UnequipItem(itemToEquip.ItemObj.ItemEnum);


        EquipAndUpdateSpriteRenderItem(itemToEquip);

        /*
        for (int i = 0; i < playerInventoryItens.Count; i++)
        {
            if (itemToEquip == playerInventoryItens[i])
            {
                
                //Remove old item equipped
                if (playerInventoryItens[i].IsEquipped)
                {
                    playerInventoryItens[i].IsEquipped = false;
                }
                

                //Set new item as equipped
                playerInventoryItens[i].IsEquipped = true;


                EquipAndUpdateSpriteRenderItem(itemToEquip);
                // Update the inventory item list 
                InventoryManager.Instance.UpdateInventoryItensUI();
                break;
            }
        }
        */
        InventoryManager.Instance.UpdateInventoryItensUI();


    }

    private void EquipAndUpdateSpriteRenderItem(Item itemToEquip)
    {
        //Update the item sprite in the correct spriteRender for the item
        for (int i = 0; i < playerEquipments.Length; i++)
        {
            if (itemToEquip.ItemObj.ItemEnum == playerEquipments[i].itemID)
            {
                playerEquipments[i].spriteRender.sprite = itemToEquip.ItemObj.Sprite;
                playerEquipments[i].item = itemToEquip;
                playerEquipments[i].item.IsEquipped = true;
            }
        }
    }

    // Unequip the item that is equipped for the specific slot
    public void UnequipItem(ItemObject.ItemID itemID)
    {
        for (int i = 0; i < playerEquipments.Length; i++)
        {
            if (playerEquipments[i].itemID == itemID && playerEquipments[i].item != null)
            {              
                playerEquipments[i].item.IsEquipped = false;
                playerEquipments[i].item = null;

                //Update the equipment sprite for its default
                playerEquipments[i].spriteRender.sprite = playerEquipments[i].defaultSprite;
            }
        }
        InventoryManager.Instance.UpdateInventoryItensUI();
    }

    // Add item to inventory and equipped it
    public void AddItemToInventory(Item itemToAdd)
    {
        playerInventoryItens.Add(itemToAdd);
        //EquipItem(itemToAdd);
        // Update the inventory item list
        InventoryManager.Instance.UpdateInventoryItensUI();
    }

    public void RemoveItemFromInventory(Item itemToRemove)
    {
        for (int i = 0; i < playerInventoryItens.Count; i++)
        {
            if (playerInventoryItens[i] == itemToRemove)
            {
                //If item is equipped, it needs to be unequipped first
                if(itemToRemove.IsEquipped)
                {
                    UnequipItem(itemToRemove.ItemObj.ItemEnum);
                }
                playerInventoryItens.RemoveAt(i);               
            }
        }
    }
}

