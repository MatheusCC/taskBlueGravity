using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

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
}
