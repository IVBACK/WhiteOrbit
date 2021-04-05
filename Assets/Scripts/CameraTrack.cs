using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    Player player;
    
    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    
    void Update()
    {
        if(player != null)
        {
            Vector2 playerPos = player.transform.position;
            transform.position = playerPos;
        }       
    }
}
