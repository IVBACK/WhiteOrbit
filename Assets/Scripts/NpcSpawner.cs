using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    [SerializeField] GameObject npcForSpawn;

    [SerializeField] int maxNpcCount;

    private int npcCount;

    void Start()
    {
        StartCoroutine(CheckAliensOnScene());
    }

    IEnumerator CheckAliensOnScene()
    {
        while (true)
        {
            npcCount = transform.childCount;

            if (npcCount < maxNpcCount)
            {
                StartCoroutine(SpawnAliens());
                yield return new WaitForSeconds(20);
            }
            else
            {
                yield return new WaitForSeconds(20);
            }
        }      
    }

    IEnumerator SpawnAliens()
    {
        while(npcCount < maxNpcCount)
        {
            npcCount++;
            GameObject alien = Instantiate(npcForSpawn, transform.position + new Vector3(Random.Range(-60f, 60f), Random.Range(-40f, 40f), 10), Quaternion.identity);
            alien.transform.parent = gameObject.transform;
            yield return new WaitForSeconds(2);
        }
        StopCoroutine(SpawnAliens());
    }
}
