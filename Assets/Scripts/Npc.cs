using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{      
    private Vector3 target;
    private Vector3 randomPos;
    private Vector3 posOffset;
    
    [HideInInspector] public Vector3 movement;
    [HideInInspector] public Vector3 targetLastPos;   

    [SerializeField] int exp;
    [SerializeField] float speed = 1f;    
    [SerializeField] float shootDelay = 1f;

    [HideInInspector] public bool targetCycle;
    [HideInInspector] public bool patrol = true;
    [HideInInspector] public bool aggro = false;
    [HideInInspector] public bool trackTarget = false;

    private bool isFiring;  

    [SerializeField] GameObject laser;
    [SerializeField] GameObject gun;

    [HideInInspector] public TargetSystem targetSystem;

    private Quaternion toTargetRotation;

    private void Awake()
    {
        targetSystem = GetComponent<TargetSystem>();
    }

    public virtual void RandomMovement()
    {
        if (patrol != true) { return; }
        movement = randomPos;
        transform.position = Vector3.MoveTowards(transform.position, randomPos, Time.deltaTime * 1);
        if (transform.position == randomPos)
        {
            randomPos = transform.parent.transform.position + new Vector3(Random.Range(-60f, 60f), Random.Range(-40f, 40f), 10);
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
            isFiring = true;
            yield return new WaitForSeconds(shootDelay);
            GameObject laserP = Instantiate(laser, gun.transform.position, Quaternion.identity) as GameObject;
            laserP.transform.rotation = toTargetRotation;
        }
        isFiring = false;
    }

    public virtual void Rotate()
    {
        var relativePos = movement - transform.position;
        var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        toTargetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = toTargetRotation;
    }

    public virtual void TrackTarget() //Currently disabled.
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
            targetCycle = true;
        }
    }

    public virtual void HandleTargeting()
    {
        int i = 0;
        if (targetCycle != true) { return; }
        if (targetSystem.targets.Count <= 0)
        {
            StopShoot();
            aggro = false;
            targetSystem.SetLockStateFalse();
            patrol = true;
            targetCycle = false;
            targetSystem.SetTargetCrossOff();
        }
        else
        {
            patrol = false;
            targetSystem.SetLockStateTrue();
            StartPickPosOffset();
            StartShoot();                        
            GameObject target = targetSystem.targets[i];
            targetSystem.targetObject = target;
            target.GetComponent<TargetSystem>().SetTargetCrossOn();
            targetCycle = false;
        }
    }

    public void StartShoot()
    {
        if(isFiring != false) { return; }
        StartCoroutine(ShootLaser());
    }

    public void StopShoot()
    {
        StopCoroutine(ShootLaser());
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
