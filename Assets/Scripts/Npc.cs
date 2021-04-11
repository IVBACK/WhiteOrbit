using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    
    Vector3 movement;
    Vector3 target;
    Vector3 lastPlayerPos;
    Vector3 randomPos;

    [SerializeField] int exp;
    [SerializeField] float speed = 1f;    
    [SerializeField] float shootDelay = 1f;

    [SerializeField] bool patrol = true;
    [SerializeField] bool aggro = false;
    [SerializeField] bool trackPlayer = false;

    [SerializeField] GameObject targetCross;

    [SerializeField] GameObject laser;
    [SerializeField] GameObject gun;

    Quaternion toTargetRotation;

    private void Awake()
    {
        SetTargetCrossOff();
        randomPos = new Vector3(Random.Range(-60f, 60f), Random.Range(-40f, 40f));       
    }

    public virtual void RandomMovement()
    {
        if (patrol != true) { return; }
        movement = randomPos;
        transform.position = Vector3.MoveTowards(transform.position, randomPos, Time.deltaTime * 1);
        if (transform.position == randomPos)
        {
            randomPos = new Vector3(Random.Range(-60f, 60f), Random.Range(-40f, 40f));
        }     
    }

    public virtual void Aggro()
    {
        if(aggro != true) { return; }
        {
            target = FindObjectOfType<Player>().transform.position;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            movement = target;                     
        }
    }

    IEnumerator ShootLaser()
    {
        while (aggro)
        {
            GameObject laserP = Instantiate(laser, gun.transform.position, Quaternion.identity) as GameObject;
            laserP.transform.rotation = toTargetRotation;
            yield return new WaitForSeconds(shootDelay);           
        }
    }

    public virtual void Rotate()
    {
        var relativePos = movement - transform.position;
        var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        toTargetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = toTargetRotation;
    }

    public virtual void TrackPlayer()
    {
        if (trackPlayer != true) { return; }
        
        if(transform.position != lastPlayerPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastPlayerPos, speed * Time.deltaTime);
        }
        else
        {
            patrol = true;
            trackPlayer = false;
        }
    }

    public void SetTargetCrossOn()
    {
        targetCross.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void SetTargetCrossOff()
    {
        targetCross.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            aggro = true;
            patrol = false;
            //collision.GetComponent<TargetSystem>().SetTargetedStateTrue(); //Causing bug couldnt fix yet
            GetComponent<TargetSystem>().SetLockStateTrue();
            StartCoroutine(ShootLaser());
        }
    }

    private  void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            aggro = false;
            collision.GetComponent<TargetSystem>().SetTargetedStateFalse();
            GetComponent<TargetSystem>().SetLockStateFalse();
            lastPlayerPos = FindObjectOfType<Player>().transform.position;
            movement = lastPlayerPos;
            trackPlayer = true;
        }        
    }

    private void OnDestroy()
    {
        Player player = FindObjectOfType<Player>();
        if(player != null)
        {
            player.GetComponent<Player>().SetPlayerLockStateFalse();
            player.GetComponent<Level>().GetExp(exp);
        }       
    }
}
