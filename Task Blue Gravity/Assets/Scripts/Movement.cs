using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1;
    private float horizontalInput;
    private float verticalInput;
    private Vector2 movement = Vector2.zero;

    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Player inputs to move character
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        movement = new Vector2(horizontalInput, verticalInput);
        FlipPlayer();
        AnimationUpdate();

        //Debug.Log("Horizontal: " + horizontalInput + " | Vertical: " +verticalInput);

        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    // Check with direction it player is going to flip player 
    private void FlipPlayer()
    {
        Vector3 newScale = transform.localScale;

        if (horizontalInput < 0 )
        {
            newScale.x = 2f;
        }
        else if(horizontalInput > 0)
        {
            newScale.x = -2f;
        }

        transform.localScale = newScale;
    }

    //Update character animation
    private void AnimationUpdate()
    {      
        //Use always a positive value to change the animation parameter
        if (verticalInput < 0 || verticalInput > 0)
        {
            myAnimator.SetFloat("moveSpeed", Mathf.Abs(verticalInput));
        }
        else
        {
            myAnimator.SetFloat("moveSpeed", Mathf.Abs(horizontalInput));
        }       
    }
}
