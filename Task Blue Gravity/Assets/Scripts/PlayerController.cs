using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    private struct Equipments
    {
        public ItemObject.ItemID itemID;
        public SpriteRenderer equipSprite;
        public Sprite defaultSprite;
    }

    [Header("Player Default Sprites")]
    [SerializeField]
    private Sprite body = null;
    [SerializeField]
    private Sprite helmet = null;
    [SerializeField]
    private Sprite shield = null;
    [SerializeField]
    private Sprite weapon = null;

    [SerializeField]
    private Equipments[] equipments = null;

    [SerializeField]
    private List<ItemObject> playerItens = new List<ItemObject>();

    public List<ItemObject> PlayerItens
    {
        get { return playerItens; }
    }

    private void Awake()
    {
        EquipItens();
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


    private void EquipItens()
    {
        if(playerItens != null)
        {
            for (int i = 0; i < playerItens.Count; i++)
            {
                for (int y = 0; y < equipments.Length; y++)
                {
                    if (playerItens[i].ItemEnum == equipments[y].itemID)
                    {
                        equipments[y].equipSprite.sprite = playerItens[i].ItemSprite;
                        break;
                    }
                }
            }
        }
    }

    private void EquipItem(ItemObject itemToAdd)
    {
        for (int i = 0; i < equipments.Length; i++)
        {
            if (itemToAdd.ItemEnum == equipments[i].itemID)
            {
                equipments[i].equipSprite.sprite = itemToAdd.ItemSprite;
                break;
            }
        }
    }

    private void UnequipItem(ItemObject itemToUnequip)
    {
        for (int i = 0; i < equipments.Length; i++)
        {
            if (itemToUnequip.ItemEnum == equipments[i].itemID)
            {
                equipments[i].equipSprite.sprite = equipments[i].defaultSprite;
                break;
            }
        }
    }

    public void AddItemToInventory(ItemObject itemToAdd)
    {
        playerItens.Add(itemToAdd);
        EquipItem(itemToAdd);

        MenuManager.Instance.PlayerInventory.UpdateInventoryItensUI();
    }

    public void RemoveItemFromInventory(ItemObject itemToRemove)
    {
        for (int i = 0; i < playerItens.Count; i++)
        {
            if (playerItens[i] == itemToRemove)
            {
                playerItens.RemoveAt(i);
                UnequipItem(itemToRemove);
            }
        }
    }
}

