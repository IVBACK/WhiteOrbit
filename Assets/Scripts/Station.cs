using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    private bool aggro = false;
    [SerializeField] float shootDelay;

    [SerializeField] GameObject rocketP;
    [SerializeField] GameObject gun;

    IEnumerator ShootRocket()
    {
        while (aggro)
        {
            GameObject rocket = Instantiate(rocketP, gun.transform.position, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(shootDelay);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Npc>())
        {
            aggro = true;            
            collision.GetComponent<TargetSystem>().SetTargetedStateTrue();
            StartCoroutine(ShootRocket());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Npc>())
        {
            aggro = false;
            collision.GetComponent<TargetSystem>().SetTargetedStateFalse();
            GetComponent<TargetSystem>().SetLockStateFalse();
        }
    }   
}
