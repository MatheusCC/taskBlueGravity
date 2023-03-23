using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    [System.Serializable]
    public struct ShopItemSlot
    {
        public GameObject slot;
        public Image itemImage;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI priceText;
        public int itemIndex;
    }

    //[SerializeField]
    [SerializeField]
    private ShopItemSlot[] shopItensSlots = null;
    [SerializeField]
    private List<Item> shopItemList;
    private PlayerController playerController;

    public List<Item> ShopItems
    {
        get { return shopItemList; }
        set { shopItemList = value; }
    }

    private void Awake()
    {       
        if(Instance == null)
        {
            Instance = this;
        }
        
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    //Create the shop UI itens from the NPC list of itens
    private void CreateShopItens()
    {
        if(shopItemList != null)
        {
            for (int i = 0; i < shopItensSlots.Length; i++)
            {
                // Create shop slots depending the NPC itens quantity
                if(i < shopItemList.Count)
                {
                    // Update shop slot item UI with the item details
                    shopItensSlots[i].itemImage.sprite = shopItemList[i].ItemObj.Sprite;
                    shopItensSlots[i].nameText.text = shopItemList[i].ItemObj.Name;
                    shopItensSlots[i].priceText.text = shopItemList[i].ItemObj.Price.ToString();
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

    public void OpenShopPanel()
    {
        CreateShopItens();
    }

    //Buy the item from the shop
    public void BuyItem(int index)
    {
        int itemPrice = shopItemList[index].ItemObj.Price;
        if (HasEnoughtMoneyToBuy(itemPrice))
        {
            //Decrease players money
            GameManager.Instance.MoneySpent(itemPrice);

            //Add the item to the player inventory
            playerController.AddItemToInventory(shopItemList[index]);

            //Remove the item bought from the shop and update the shop UI's
            shopItemList.RemoveAt(index);
            UpdateShopItensUI();
        }
    }

    public void AddItemToShop(Item itemToAdd)
    {
        //Add item to shop and update the list of itens
        shopItemList.Add(itemToAdd); 
        UpdateShopItensUI();
    }

    private bool HasEnoughtMoneyToBuy(int itemPrice)
    {
        return GameManager.Instance.CurrentPlayerMoney >= itemPrice;
    }

    public void CloseShop()
    {
        MenuManager.Instance.CloseShop();
    }
    
}
