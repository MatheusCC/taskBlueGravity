using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private int playerMoney = 1000;

    private bool playerInputOn;

    public bool PlayerInputOn
    {
        get { return playerInputOn; }
    }

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

        playerInputOn = true;
    }

    private void Start()
    {
        // Open Start Game Info Panel
        MenuManager.Instance.OpenStartGamePanel();
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

    public void StartGame()
    {
        EnablePlayerInput(true);
    }

    public void EnablePlayerInput(bool isOn)
    {
        playerInputOn = isOn;
    }
}
