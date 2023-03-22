using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [System.Serializable]
    public struct ShopItem
    {
        public GameObject itemGameObj;
        public Image itemImage;
        public TextMeshProUGUI itemNameText;
        public TextMeshProUGUI itemPriceText;
        public int itemIndex;
    }

    [SerializeField]
    private List<ItemObject> itemObj;
    [SerializeField]
    private ShopItem[] shopItens = null;
    [SerializeField]
    private Inventory inventory = null;

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
        if(itemObj != null)
        {
            for (int i = 0; i < shopItens.Length; i++)
            {
                if(i < itemObj.Count)
                {
                    shopItens[i].itemImage.sprite = itemObj[i].ItemSprite;
                    shopItens[i].itemNameText.text = itemObj[i].ItemName;
                    shopItens[i].itemPriceText.text = itemObj[i].ItemPrice.ToString();
                    shopItens[i].itemIndex = i;
                    shopItens[i].itemGameObj.SetActive(true);
                }
                else
                {
                    // Disable other item fields from the shop if there is not more itens to add
                    shopItens[i].itemGameObj.SetActive(false);
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
        if (HasEnoughtMoneyToBuy(itemObj[index].ItemPrice))
        {
            //Decrease players money
            GameManager.Instance.MoneySpent(itemObj[index].ItemPrice);

            //Add the item to the player inventory
            playerController.AddItemToInventory(itemObj[index]);

            //Remove the item bought from the shop and update the shop UI's
            itemObj.RemoveAt(index);
            UpdateShopItensUI();
        }
    }

    public void AddItemToShop(ItemObject itemToAdd)
    {
        //Add item to shop and update the list of itens
        itemObj.Add(itemToAdd); 
        UpdateShopItensUI();
    }

    private bool HasEnoughtMoneyToBuy(int itemPrice)
    {
        return GameManager.Instance.CurrentPlayerMoney >= itemPrice;
    }
    
}
