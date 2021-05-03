using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour
{
    [SerializeField] int healAmount = 100;

    [SerializeField] float timer = 3f;

    [SerializeField] bool isPlayerDocked = false;

    [SerializeField] GameObject U�;
    
    [SerializeField] bool u�State;

    GameObject playerShip;

    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (isPlayerDocked != true) { return; }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SetU�();
        }
    }

    public void SetU�()
    {
        Debug.Log("Docked");
        if (u�State == true)
        {
            U�.SetActive(false);
            u�State = false;
            isPlayerDocked = false;
        }
        else
        {
            U�.SetActive(true);
            u�State = true;
            isPlayerDocked = false;
            FindObjectOfType<Shop>().DisplayPlayerCurrency();
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Health playerHealth = collision.GetComponent<Health>();
            playerShip = collision.gameObject;
            isPlayerDocked = true;
            playerShip.GetComponent<Health>().Heal(healAmount);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            isPlayerDocked = false;
            timer = 3f;
        }
    }
}
