using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedStation : MonoBehaviour
{
    [SerializeField] float stationRespawnCount;
    [SerializeField] GameObject stationForRespawn;

    private void Update()
    {
        stationRespawnCount -= Time.deltaTime;
        if(stationRespawnCount <= 0)
        {
            Instantiate(stationForRespawn, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
