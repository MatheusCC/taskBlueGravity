using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [System.Serializable]
    public struct ShopItemSlot
    {
        public GameObject slot;
        public Image itemImage;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI priceText;
        public int itemIndex;
    }

    [SerializeField]
    private List<Item> shopItem;
    [SerializeField]
    private ShopItemSlot[] shopItensSlots = null;
    /*
    [SerializeField]
    private Inventory inventory = null;
    */

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //CreateShopItens();
    }



    // Start is called before the first frame update
    private void OnEnable()
    {
        CreateShopItens();
    }

    //Create the shop itens from the list of objects
    private void CreateShopItens()
    {
        if(shopItem != null)
        {
            for (int i = 0; i < shopItensSlots.Length; i++)
            {
                if(i < shopItem.Count)
                {
                    shopItensSlots[i].itemImage.sprite = shopItem[i].ItemObj.Sprite;
                    shopItensSlots[i].nameText.text = shopItem[i].ItemObj.Name;
                    shopItensSlots[i].priceText.text = shopItem[i].ItemObj.Price.ToString();
                    shopItensSlots[i].itemIndex = i;
                    shopItensSlots[i].slot.SetActive(true);
                }
                else
                {
                    // Disable other item fields from the shop if there is not more itens to add
                    shopItensSlots[i].slot.SetActive(false);
                }
            }
        }
    }

    private void UpdateShopItensUI()
    {
        CreateShopItens();
    }

    //Buy the item from the shop
    public void BuyItem(int index)
    {
        int itemPrice = shopItem[index].ItemObj.Price;
        if (HasEnoughtMoneyToBuy(itemPrice))
        {
            //Decrease players money
            GameManager.Instance.MoneySpent(itemPrice);

            //Add the item to the player inventory
            playerController.AddItemToInventory(shopItem[index]);

            //Remove the item bought from the shop and update the shop UI's
            shopItem.RemoveAt(index);
            UpdateShopItensUI();
        }
    }

    public void AddItemToShop(Item itemToAdd)
    {
        //Add item to shop and update the list of itens
        shopItem.Add(itemToAdd); 
        UpdateShopItensUI();
    }

    private bool HasEnoughtMoneyToBuy(int itemPrice)
    {
        return GameManager.Instance.CurrentPlayerMoney >= itemPrice;
    }
    
}
