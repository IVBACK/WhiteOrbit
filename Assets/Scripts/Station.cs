using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    private bool aggro = false;
    private bool targetCycle;

    [SerializeField] int exp;
    [SerializeField] float shootDelay;

    [SerializeField] GameObject rocketP;
    [SerializeField] GameObject gun;

    private TargetSystem targetSystem;

    private void Start()
    {
        targetSystem = GetComponent<TargetSystem>();
    }

    private void Update()
    {
        HandleTargeting();
    }

    IEnumerator ShootRocket()
    {
        while (aggro)
        {
            GameObject rocket = Instantiate(rocketP, gun.transform.position, Quaternion.identity) as GameObject;
            rocket.transform.parent = this.gameObject.transform;
            yield return new WaitForSeconds(shootDelay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<NpcAlien>())
        {
            targetSystem.targets.Add(collision.gameObject);
            targetCycle = true;
            aggro = true;
            StartCoroutine(ShootRocket());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        targetSystem.targets.Remove(collision.gameObject);
        if (collision.GetComponent<NpcAlien>())
        {
            targetCycle = true;
        }
    }

    private void HandleTargeting()
    {
        int i = 0;
        if (targetCycle != true) { return; }
        if(targetSystem.targets.Count <= 0)
        {
            aggro = false;
            StopCoroutine(ShootRocket());
            targetCycle = false;
        }
        else
        {
            GameObject target = targetSystem.targets[i];
            targetSystem.targetObject = target;
            targetCycle = false;
        }
    }

    private void OnDestroy()
    {
        Player player = FindObjectOfType<Player>();
        if (targetSystem.isTargetedByPlayer)
        {
            player.GetComponent<Player>().SetPlayerLockStateFalse();
            player.GetComponent<Level>().GetExp(exp);
            player.GetComponent<Currency>().AddCurrency(Random.Range(3, 10));
        }      
    }
}
