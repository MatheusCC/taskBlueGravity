using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public struct InventoryItem
    {
        public GameObject itemGameObj;
        public Image itemImage;
        public TextMeshProUGUI itemNameText;
        public TextMeshProUGUI itemPriceText;
        public int itemIndex;
    }

    [SerializeField]
    private Shop shop = null;

    [SerializeField]
    private InventoryItem[] inventoryItens = null;

    private PlayerController playerController;

    private void Awake()
    {
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
        if (playerController.PlayerItens != null)
        {
            for (int i = 0; i < inventoryItens.Length; i++)
            {
                if (i < playerController.PlayerItens.Count)
                {
                    inventoryItens[i].itemImage.sprite = playerController.PlayerItens[i].ItemSprite;
                    inventoryItens[i].itemNameText.text = playerController.PlayerItens[i].ItemName;
                    inventoryItens[i].itemPriceText.text = playerController.PlayerItens[i].ItemPrice.ToString();
                    inventoryItens[i].itemIndex = i;
                    inventoryItens[i].itemGameObj.SetActive(true);
                }
                else
                {
                    // Disable other item fields from the shop if there is not more itens to add
                    inventoryItens[i].itemGameObj.SetActive(false);
                }
            }
        }
    }

    public void UpdateInventoryItensUI()
    {
        CreateInventoryItens();
    }

    //Buy the item from the shop
    public void SellItem(int itemIndex)
    {
        //Add the item to the shop
        shop.AddItemToShop(playerController.PlayerItens[itemIndex]);

        //Remove the item from inventory and add to the shop and update the shop UI's
        playerController.RemoveItemFromInventory(playerController.PlayerItens[itemIndex]);
        UpdateInventoryItensUI();
    }


}
