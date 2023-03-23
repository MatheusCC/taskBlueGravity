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

            if (Input.GetKeyDown(KeyCode.F))
            {
                OpenShop();
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
