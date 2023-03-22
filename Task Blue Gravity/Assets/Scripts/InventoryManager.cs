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
        public int itemIndex;
    }

    [SerializeField]
    private Shop shop = null;
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
        //CreateInventoryItens();
    }

    //Create the shop itens from the list of objects
    private void CreateInventoryItens()
    {
        if (playerController.PlayerInventoryItens != null)
        {
            for (int i = 0; i < inventoryItens.Length; i++)
            {
                if (i < playerController.PlayerInventoryItens.Count)
                {
                    inventoryItens[i].itemIcon.sprite = playerController.PlayerInventoryItens[i].ItemObj.Sprite;
                    inventoryItens[i].itemNameText.text = playerController.PlayerInventoryItens[i].ItemObj.Name;
                    inventoryItens[i].itemPriceText.text = playerController.PlayerInventoryItens[i].ItemObj.Price.ToString();
                    inventoryItens[i].equipItemButton.interactable = !playerController.PlayerInventoryItens[i].IsEquipped;
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

    //Update list of itens and player current money text
    public void UpdateInventoryItensUI()
    {
        CreateInventoryItens();
        
        playerMoneyText.text = GameManager.Instance.CurrentPlayerMoney.ToString();
    }

    //Buy the item from the shop
    public void SellItem(int itemIndex)
    {
        //Increase player money
        GameManager.Instance.MoneyEarned(playerController.PlayerInventoryItens[itemIndex].ItemObj.Price);

        //Add the item to the shop
        shop.AddItemToShop(playerController.PlayerInventoryItens[itemIndex]);

        //Remove the item from inventory and add to the shop and update the shop UI's
        playerController.RemoveItemFromInventory(playerController.PlayerInventoryItens[itemIndex]);
        UpdateInventoryItensUI();
    }

    public void EquipItem(int itemIndex)
    {
        playerController.EquipItem(playerController.PlayerInventoryItens[itemIndex]);
    }
}
