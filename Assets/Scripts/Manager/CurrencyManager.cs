using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    private static CurrencyManager instance;
    public static CurrencyManager Instance { get { return instance; } }

    public int currency;

    [SerializeField]
    private int initialCurrency;


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

    public void ResetCurrency()
    {
        currency = initialCurrency;
    }

}
