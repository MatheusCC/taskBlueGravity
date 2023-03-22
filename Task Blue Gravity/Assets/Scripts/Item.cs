using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemObject item;
    [SerializeField]
    private bool isEquipped = false;

    public ItemObject ItemObj
    {
        get { return item; }
    }

    public bool IsEquipped
    {
        get { return isEquipped; }
        set { isEquipped = value; }
    }

    private void Awake()
    {
        isEquipped = false;
    }
}
