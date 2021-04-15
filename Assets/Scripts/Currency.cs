using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    int playerTotalCurrency;

    [SerializeField] Text currencyText;

    public void AddCurrency(int currencyForAdd)
    {
        playerTotalCurrency += currencyForAdd;
        UpdateCurrencyDisplay();
    }

    public bool SpendCurrency(int currencyForSpend)
    {
        if(playerTotalCurrency >= currencyForSpend)
        {
            playerTotalCurrency -= currencyForSpend;           
            UpdateCurrencyDisplay();
            return true;
        }
        else
        {
            return false;            
        }
    }

    private void UpdateCurrencyDisplay()
    {
        currencyText.text = playerTotalCurrency.ToString();
    }

    public int ReturnPlayerTotalCurreny()
    {
        return playerTotalCurrency;
    }
}


