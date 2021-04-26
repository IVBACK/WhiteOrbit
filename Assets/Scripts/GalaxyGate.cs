using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GalaxyGate : MonoBehaviour
{
    [SerializeField] GameObject warpTarget;
    
    [SerializeField] float timer = 3f;

    private bool player = false;

    private GameObject playerShip;
    
    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if(player != true) { return; }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            playerShip.transform.position = warpTarget.transform.position;
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
            collision.GetComponent<Player>().isClicked = false;
            timer = 3f;           
        }
    }
}
