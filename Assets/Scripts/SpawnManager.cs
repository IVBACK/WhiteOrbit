using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Vector3 spawn;

    public void SetSpawn(Vector3 spawnPos)
    {
        spawn = spawnPos;
    }   

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.Find("PlayerShip");
        player.GetComponent<Player>().isClicked = false;
        player.transform.position = spawn;      
    }
}
