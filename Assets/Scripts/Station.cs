using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    private bool aggro = false;

    public bool cycle;

    [SerializeField] int exp;
    [SerializeField] float shootDelay;

    [SerializeField] GameObject rocketP;
    [SerializeField] GameObject gun;

    TargetSystem targetSystem;

    public List<GameObject> targets = new List<GameObject>();

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
        if (collision.GetComponent<Npc>())
        {
            targets.Add(collision.gameObject);
            cycle = true;
            aggro = true;
            StartCoroutine(ShootRocket());

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        targets.Remove(collision.gameObject);
        if (collision.GetComponent<Npc>() && collision.GetComponent<TargetSystem>().ReturnTargetedState())
        {
            cycle = true;
        }
    }

    private void HandleTargeting()
    {
        int i = 0;
        if (cycle != true) { return; }
        if(targets.Count <= 1)
        {
            Debug.Log("No Target");           
            aggro = false;
            StopCoroutine(ShootRocket());
            cycle = false;

        }
        else
        {
            GameObject target = targets[i + 1];
            targetSystem.targetObject = target;
            target.GetComponent<TargetSystem>().SetTargetCrossOn();
            target.GetComponent<TargetSystem>().SetTargetedStateTrue();
            cycle = false;
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
