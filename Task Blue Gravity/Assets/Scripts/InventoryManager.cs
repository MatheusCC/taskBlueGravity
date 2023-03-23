using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    [System.Serializable]
    public struct InventoryItem
    {
        public GameObject inventorySlot;
        public Image itemIcon;
        public TextMeshProUGUI itemNameText;
        public TextMeshProUGUI itemPriceText;
        public Button equipItemButton;
        public TextMeshProUGUI equipItemButtonText;
        public int itemIndex;
    }

    [SerializeField]
    private InventoryItem[] inventoryItens = null;
    [SerializeField]
    private TextMeshProUGUI playerMoneyText = null;

    private PlayerController playerController;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        CreateInventoryItens();
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        CreateInventoryItens();
    }

    //Create the inventory itens from the list of objects
    private void CreateInventoryItens()
    {
        if (playerController.PlayerInventoryItens != null)
        {
            for (int i = 0; i < inventoryItens.Length; i++)
            {
                // Create inventory slots depending on the player itens in the inventory 
                if (i < playerController.PlayerInventoryItens.Count)
                {
                    // If the item is equipped or not,
                    // Update the UI button to corret indicate
                    UpdateItemButtonUI(i, playerController.PlayerInventoryItens[i].IsEquipped);

                    // Update inventory slot item UI with the item details
                    inventoryItens[i].itemIcon.sprite = playerController.PlayerInventoryItens[i].ItemObj.Sprite;
                    inventoryItens[i].itemNameText.text = playerController.PlayerInventoryItens[i].ItemObj.Name;
                    inventoryItens[i].itemPriceText.text = playerController.PlayerInventoryItens[i].ItemObj.Price.ToString();
                    inventoryItens[i].itemIndex = i;
                    inventoryItens[i].inventorySlot.SetActive(true);
                }
                else
                {
                    // Disable other item fields from the shop if there is not more itens to add
                    inventoryItens[i].inventorySlot.SetActive(false);
                }
            }
        }
    }

    //Update the inventory list of itens UI
    public void UpdateInventoryItensUI()
    {
        CreateInventoryItens();

        UpdatePlayerMoneyText();
    }

    //Sell the item back to the shop
    public void SellItem(int itemIndex)
    {
        if (MenuManager.Instance.IsShopOpen())
        {
            //Increase player money
            GameManager.Instance.MoneyEarned(playerController.PlayerInventoryItens[itemIndex].ItemObj.Price);
            playerMoneyText.text = GameManager.Instance.CurrentPlayerMoney.ToString();

            //Add the item to the shop
            ShopManager.Instance.AddItemToShop(playerController.PlayerInventoryItens[itemIndex]);

            //Remove the item from inventory and add to the shop and update the shop UI's
            playerController.RemoveItemFromInventory(playerController.PlayerInventoryItens[itemIndex]);
            UpdateInventoryItensUI();
        }
    }

    public void EquipOrRemoveItem(int itemIndex)
    {
        Item item = playerController.PlayerInventoryItens[itemIndex];

        //Equip item if not equipped
        if (!item.IsEquipped)
        {
            // remove any item equipped for the specific ID
            playerController.UnequipItem(item.ItemObj.ItemEnum);

            // then equipe the new item
            playerController.EquipItem(item);
        }
        else
        {
            // remove any item equipped for the specific ID
            playerController.UnequipItem(item.ItemObj.ItemEnum);
        }
    }

    private void UpdateItemButtonUI(int itemIndex, bool isEquipped)
    {
        if (isEquipped)
        {
            inventoryItens[itemIndex].equipItemButtonText.text = "Remove";
            inventoryItens[itemIndex].equipItemButton.image.color = Color.gray;
        }
        else
        {
            inventoryItens[itemIndex].equipItemButtonText.text = "Equip";
            inventoryItens[itemIndex].equipItemButton.image.color = Color.green;
        }
    }

    private void UpdatePlayerMoneyText()
    {
        playerMoneyText.text = GameManager.Instance.CurrentPlayerMoney.ToString();
    }
}
