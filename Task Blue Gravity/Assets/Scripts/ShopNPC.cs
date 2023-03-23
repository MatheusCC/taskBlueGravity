using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    [SerializeField]
    private List<Item> shopItemList = null;
    [SerializeField]
    private GameObject chatPanel = null;
    [SerializeField]
    private float distanceToInteract = 1.0f;

    private Transform player;
    private bool isShopOpen;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isShopOpen = false;

        // Set all shop item to not equipped to avoid erros
        for (int i = 0; i < shopItemList.Count; i++)
        {
            shopItemList[i].IsEquipped = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(IsInDistanceToInteract())
        {
            EnableChatPanel(true);
            // With the click of the mouse left button
            // create a raycast down from the mouse position and detect if the shop was clicked;
            if (Input.GetKeyDown(KeyCode.F))
            {

                OpenShop();
                /*
                RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);

                if (hit2D.collider != null)
                {
                    //Open the shop if the shop as clicked
                    if (hit2D.collider.gameObject.CompareTag("Merchant"))
                    {
                        MenuManager.Instance.OpenShop();
                    }
                }
                */
            }
        }
        else
        {
            EnableChatPanel(false);
        }
    }

    private void EnableChatPanel(bool isOn)
    {
        chatPanel.SetActive(isOn);
    }

    private bool IsInDistanceToInteract()
    {
        float distance = Vector2.Distance(this.transform.position, player.transform.position);
        return distance <= distanceToInteract; 
    }

    private void OpenShop()
    {
        ShopManager.Instance.ShopItems = shopItemList;
        MenuManager.Instance.OpenShop();
    }
}
