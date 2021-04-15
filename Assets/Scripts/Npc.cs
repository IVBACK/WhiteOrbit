using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{

    public Vector3 movement;
    Vector3 target;
    public Vector3 targetLastPos;
    Vector3 randomPos;

    public GameObject targetGameObject;

    [SerializeField] int exp;
    [SerializeField] float speed = 1f;    
    [SerializeField] float shootDelay = 1f;

    public bool patrol = true;
    public bool aggro = false;
    public bool trackPlayer = false;

    [SerializeField] GameObject laser;
    [SerializeField] GameObject gun;

    Quaternion toTargetRotation;

    private void Awake()
    {
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
        if (aggro != true) { return; }
        {
            target = targetGameObject.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, target + new Vector3(3, 3, 0), speed * Time.deltaTime);
            movement = target;
        }
    }

    public IEnumerator ShootLaser()
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

    public virtual void TrackTarget()
    {
        if (trackPlayer != true) { return; }
        
        if(transform.position != targetLastPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetLastPos, speed * Time.deltaTime);
        }
        else
        {
            patrol = true;
            trackPlayer = false;
        }
    }

    public void StartShoot()
    {
        StartCoroutine(ShootLaser());
    }  

    private void OnDestroy()
    {
        Player player = FindObjectOfType<Player>();
        if(player != null)
        {
            player.GetComponent<Player>().SetPlayerLockStateFalse();
            player.GetComponent<Level>().GetExp(exp);
            player.GetComponent<Currency>().AddCurrency(Random.Range(3, 10));
        }       
    }
}
