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
        // If is in range to interact with NPC
        // open the NPC message
        if(IsInDistanceToInteract())
        {
            EnableChatPanel(true);
            // If player press F
            // open the shop UI window
            if (Input.GetKeyDown(KeyCode.F))
            {
                OpenShop();
            }
        }
        else
        {
            // If player is not in range to interact
            // disable npc message
            EnableChatPanel(false);
            // Close shop UI if it is opened
            if(isShopOpen)
            {
                CloseShop();
            }
        }
    }

    private void EnableChatPanel(bool isOn)
    {
        chatPanel.SetActive(isOn);
    }

    // Check if player is in range to interact
    private bool IsInDistanceToInteract()
    {
        float distance = Vector2.Distance(this.transform.position, player.transform.position);
        return distance <= distanceToInteract; 
    }

    //Open Shop
    private void OpenShop()
    {
        isShopOpen = true;
        // Update the shop itens UI, with the npc item 
        ShopManager.Instance.ShopItems = shopItemList;
        MenuManager.Instance.OpenShop();
    }

    private void CloseShop()
    {
        isShopOpen = false;
        MenuManager.Instance.CloseShop();
    }
}
