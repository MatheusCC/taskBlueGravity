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
    private GameObject inventory = null;




    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Open show and inventory UI's
    public void OpenShop()
    {
        shopMenu.SetActive(true);
        inventory.SetActive(true);
    }

    //Close show and inventory UI's
    public void CloseShop()
    {
        shopMenu.SetActive(false);
        inventory.SetActive(false);
    }
}
