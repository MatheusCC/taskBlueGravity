using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Sprites")]
    [SerializeField]
    private SpriteRenderer helmet = null;
    [SerializeField]
    private SpriteRenderer body = null;
    [SerializeField]
    private SpriteRenderer backArm = null;
    [SerializeField]
    private SpriteRenderer frontArm = null;
    [SerializeField]
    private SpriteRenderer shield = null;
    [SerializeField]
    private SpriteRenderer weapon = null;


    [SerializeField]
    private List<ItemObject> playerItens = new List<ItemObject>();

    public List<ItemObject> PlayerItens
    {
        get { return playerItens; }
    }



    void Update()
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

    public void AddItemToInventory(ItemObject itemToAdd)
    {
        playerItens.Add(itemToAdd);
        MenuManager.Instance.PlayerInventory.UpdateInventoryItensUI();
    }

    public void RemoveItemFromInventory(ItemObject itemToRemove)
    {
        for (int i = 0; i < playerItens.Count; i++)
        {
            if (playerItens[i] == itemToRemove)
            {
                playerItens.RemoveAt(i);
            }
        }
    }
}

