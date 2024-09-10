using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField]
    private GameUIManager gameUIManagerObj;

    public Transform startPoint;
    public Transform[] path;

    public int currency;

    [SerializeField]
    private int playerHealth;

    [SerializeField]
    private int MaxHealth;

    [SerializeField]
    private int initialCurrency;

    public bool isGameOver;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currency = initialCurrency;
        playerHealth = MaxHealth;
        gameUIManagerObj.setMaxHealth(MaxHealth);
        isGameOver = false;
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount; 
            return true;
        }
        else
        {
            Debug.Log("Insufficient Funds");
            return false;
        }
    }

    public void DecreaseHealth(int amount)
    {
        playerHealth-=amount;
        gameUIManagerObj.setHealth(playerHealth);

        if (playerHealth<=0)
        {
            playerHealth = 0;
            isGameOver = true;
            Debug.Log("Game Over");
        }
    }
}
