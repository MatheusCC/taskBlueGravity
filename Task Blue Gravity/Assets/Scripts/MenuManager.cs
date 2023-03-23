using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    
    [SerializeField]
    private GameObject shopMenu = null;
    [SerializeField]
    private GameObject inventoryMenu = null;
    [SerializeField]
    private GameObject startGamePanel = null;

    /*
    [SerializeField]
    private Inventory playerInventory = null;

    public Inventory PlayerInventory
    {
        get { return playerInventory; }
    }
    */

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        //Open or Close Inventory
        if(Input.GetKeyDown(KeyCode.I))
        {
            OpenInventory(inventoryMenu.activeInHierarchy);
        }
    }

    private void OpenInventory(bool open)
    {
        inventoryMenu.SetActive(!open);
    }

    //Open show and inventory UI's
    public void OpenShop()
    {
        shopMenu.SetActive(true);
        ShopManager.Instance.OpenShopPanel();
        inventoryMenu.SetActive(true);
    }

    //Close shop and inventory UI's
    public void CloseShop()
    {
        shopMenu.SetActive(false);
        inventoryMenu.SetActive(false);
    }

    public void OpenStartGamePanel()
    {
        GameManager.Instance.EnablePlayerInput(false);
        startGamePanel.SetActive(true);
    }

    public void CloseStartGamePanel()
    {
        startGamePanel.SetActive(false);
        GameManager.Instance.StartGame();
    }
}
