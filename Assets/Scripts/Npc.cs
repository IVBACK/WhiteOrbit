using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{ 
    Vector3 target;
    Vector3 randomPos;
    Vector3 posOffset;
    
    public Vector3 movement;
    public Vector3 targetLastPos;   

    [SerializeField] int exp;
    [SerializeField] float speed = 1f;    
    [SerializeField] float shootDelay = 1f;

    public bool patrol = true;
    public bool aggro = false;
    public bool trackTarget = false;

    [SerializeField] GameObject laser;
    [SerializeField] GameObject gun;

    public TargetSystem targetSystem;

    Quaternion toTargetRotation;

    private void Awake()
    {
        randomPos = new Vector3(Random.Range(-60f, 60f), Random.Range(-40f, 40f), 10);
        posOffset = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
        targetSystem = GetComponent<TargetSystem>();
    }

    public virtual void RandomMovement()
    {
        if (patrol != true) { return; }
        movement = randomPos;
        transform.position = Vector3.MoveTowards(transform.position, randomPos, Time.deltaTime * 1);
        if (transform.position == randomPos)
        {
            randomPos = new Vector3(Random.Range(-60f, 60f), Random.Range(-40f, 40f), 10);
        }     
    }

    public virtual void Aggro()
    {
        if (aggro != true) { return; }
        {
            if(targetSystem.targetObject != null)
            {
                target = targetSystem.targetObject.transform.position;
                transform.position = Vector3.MoveTowards(transform.position, target + posOffset, speed * Time.deltaTime);
                movement = target;
            }
        }
    }

    private IEnumerator PickPosOffset()
    {
        while(aggro)
        {
            posOffset = new Vector3(Random.Range(-15, 15), Random.Range(-15, 15), 0);
            yield return new WaitForSecondsRealtime(2);
        }      
    }

    private IEnumerator ShootLaser()
    {
        while (aggro)
        {           
            yield return new WaitForSeconds(shootDelay);
            GameObject laserP = Instantiate(laser, gun.transform.position, Quaternion.identity) as GameObject;
            laserP.transform.rotation = toTargetRotation;
        }
    }

    public virtual void Rotate()
    {
        var relativePos = movement - transform.position;
        var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        toTargetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = toTargetRotation;
    }

    public virtual void TrackTarget()
    {
        if (trackTarget != true) { return; }
        
        if(transform.position != targetLastPos)
        {
            movement = targetLastPos;
            transform.position = Vector3.MoveTowards(transform.position, targetLastPos, speed * Time.deltaTime);
        }
        else
        {
            patrol = true;
            trackTarget = false;
        }
    }

    public void StartShoot()
    {
        StartCoroutine(ShootLaser());
    }

    public void StartPickPosOffset()
    {
        StartCoroutine(PickPosOffset());
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
