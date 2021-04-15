using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour
{
    [SerializeField] float timer = 3f;

    [SerializeField] bool player = false;

    [SerializeField] GameObject U�;
    
    [SerializeField] bool u�State;

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
            player = false;
        }
        else
        {
            U�.SetActive(true);
            u�State = true;
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
