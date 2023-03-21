using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    private struct Equipments
    {
        public ItemObject.ItemID itemID;
        public SpriteRenderer spriteRender;
        public Sprite defaultSprite;
        public ItemObject itemEquipped;
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
    private List<ItemObject> playerInventoryItens = new List<ItemObject>();

    public List<ItemObject> PlayerItens
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
                    Debug.Log("OPEN SHOP!");
                }
            }
        }
    }

    private void EquipItem(ItemObject itemToAdd)
    {
        for (int i = 0; i < playerEquipments.Length; i++)
        {
            if (itemToAdd.ItemEnum == playerEquipments[i].itemID)
            {
                //Remove old item equipped
                if(playerEquipments[i].itemEquipped != null)
                {
                    playerEquipments[i].itemEquipped.IsEquipped = false;
                }

                //Set new item as equipped
                playerEquipments[i].itemEquipped = itemToAdd;
                playerEquipments[i].itemEquipped.IsEquipped = true;
                playerEquipments[i].spriteRender.sprite = itemToAdd.ItemSprite;
                break;
            }
        }
    }

    private void UnequipItem(ItemObject itemToUnequip)
    {
        for (int i = 0; i < playerEquipments.Length; i++)
        {
            if (itemToUnequip.ItemEnum == playerEquipments[i].itemID)
            {
                playerEquipments[i].spriteRender.sprite = playerEquipments[i].defaultSprite;
                break;
            }
        }
    }

    public void AddItemToInventory(ItemObject itemToAdd)
    {
        playerInventoryItens.Add(itemToAdd);
        EquipItem(itemToAdd);

        MenuManager.Instance.PlayerInventory.UpdateInventoryItensUI();
    }

    public void RemoveItemFromInventory(ItemObject itemToRemove)
    {
        for (int i = 0; i < playerInventoryItens.Count; i++)
        {
            if (playerInventoryItens[i] == itemToRemove)
            {
                //If item is equipped, it needs to be unequipped first
                if(itemToRemove.IsEquipped)
                {
                    UnequipItem(itemToRemove);
                }
                playerInventoryItens.RemoveAt(i);
                
            }
        }
    }
}

