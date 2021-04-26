using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject player;
    GameObject playerRespawnPos;

    private void Start()
    {
        player = GameObject.Find("PlayerShip");
        playerRespawnPos = GameObject.Find("PlayerRespawnPos");
    }

    private void Update()
    {
        ExitGame();
    }
    
    private void ExitGame()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            Application.Quit();
        }
    }

    public void HandlePlayerDeath()
    {
        player.GetComponent<Player>().isClicked = false;
        player.GetComponent<TargetSystem>().BreakPlayerLock();
        player.transform.position = playerRespawnPos.transform.position;
        player.GetComponent<Health>().Heal(10);
    }
}
