using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] Button rocketBuyButton;
    [SerializeField] int rocketCost;
    [SerializeField] Text rocketCostText;

    [SerializeField] Text playerCurrencyText;

    Currency currency;

    PlayerInventory playerInventory;

    private void Awake()
    {
        Player player = FindObjectOfType<Player>();
        playerInventory = player.GetComponent<PlayerInventory>();
        currency = player.GetComponent<Currency>();

        rocketCostText.text = rocketCost.ToString();
    }

    public void BuyRockets()
    {
        if(currency.SpendCurrency(rocketCost))
        {
            playerInventory.AddRocket();
            DisplayPlayerCurrency();
        }
        else
        {
            Debug.Log("Not Enough Currency");
        }
    }

    public void DisplayPlayerCurrency()
    {
        int playerTotalCurrency = currency.ReturnPlayerTotalCurreny();
        playerCurrencyText.text = playerTotalCurrency.ToString();
    }
}
