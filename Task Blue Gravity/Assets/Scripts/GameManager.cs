using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private int playerMoney = 1000;


    public int CurrentPlayerMoney
    {
        get { return playerMoney; }
    }

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



    // Decrease player money
    public void MoneySpent(int value)
    {
        playerMoney -= value;
    }

    // Increase player money
    public void MoneyEarned(int value)
    {
        playerMoney += value;
    }
}
