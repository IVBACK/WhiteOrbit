using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour
{
    [SerializeField] int healAmount = 100;

    [SerializeField] float timer = 3f;

    [SerializeField] bool isPlayerDocked = false;

    [SerializeField] GameObject Uý;
    
    [SerializeField] bool uýState;

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
            SetUý();
        }
    }

    public void SetUý()
    {
        Debug.Log("Docked");
        if (uýState == true)
        {
            Uý.SetActive(false);
            uýState = false;
            isPlayerDocked = false;
        }
        else
        {
            Uý.SetActive(true);
            uýState = true;
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
