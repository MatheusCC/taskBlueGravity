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
        public Item itemEquipped;
    }

    [SerializeField]
    private Equipments[] playerEquipments = null;
    [SerializeField]
    private List<Item> playerInventoryItens = new List<Item>();

    public List<Item> PlayerInventoryItens
    {
        get { return playerInventoryItens; }
    }

    public void EquipItem(Item itemToEquip)
    {
        // Before to equip a new item,
        // check if has a item already equipped and unequip it
        UnequipItem(itemToEquip.ItemObj.ItemEnum);

        EquipAndUpdateSpriteRenderItem(itemToEquip);
        
        InventoryManager.Instance.UpdateInventoryItensUI();
    }

    private void EquipAndUpdateSpriteRenderItem(Item itemToEquip)
    {
        // Set item as equipped
        itemToEquip.IsEquipped = true;

        //Update the item sprite in the correct spriteRender for the item
        for (int i = 0; i < playerEquipments.Length; i++)
        {
            if (itemToEquip.ItemObj.ItemEnum == playerEquipments[i].itemID)
            {
                    playerEquipments[i].spriteRender.sprite = itemToEquip.ItemObj.Sprite;
                    playerEquipments[i].itemEquipped = itemToEquip;             
            }
        }
    }

    // Unequip the item that is equipped for the specific slot
    public void UnequipItem(ItemObject.ItemID itemID)
    {
        for (int i = 0; i < playerEquipments.Length; i++)
        {
            if (playerEquipments[i].itemID == itemID && playerEquipments[i].itemEquipped != null)
            {              
                playerEquipments[i].itemEquipped.IsEquipped = false;
                playerEquipments[i].itemEquipped = null;

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

