using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour
{
    [SerializeField] float timer = 3f;

    [SerializeField] bool player = false;

    [SerializeField] GameObject Uý;
    
    [SerializeField] bool uýState;

    GameObject playerShip;

    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (player != true) { return; }
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
            player = false;
        }
        else
        {
            Uý.SetActive(true);
            uýState = true;
            player = false;
            FindObjectOfType<Shop>().DisplayPlayerCurrency();
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            playerShip = collision.gameObject;
            player = true;           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            player = false;
            timer = 3f;
        }
    }
}
