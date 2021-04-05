using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    
    Vector3 movement;
    Vector3 target;
    Vector3 lastPlayerPos;
    //Vector3 randomPos;

    [SerializeField] float speed = 1f;    
    //[SerializeField] float timer = 1;
    [SerializeField] float shootDelay = 1f;

    [SerializeField] bool aggro = false;
    bool trackPlayer = false;

    [SerializeField] GameObject laser;
    [SerializeField] GameObject gun;

    Quaternion toTargetRotation;

    //float x;
    //float y;

    /*private void RandomMovement()
    {
        if(timer <= 0)
        {
            x = Random.Range(-5f, 5f);
            y = Random.Range(-5f, 5f);
            randomPos = new Vector3(x, y);
            Debug.Log(randomPos);
            transform.position = Vector3.MoveTowards(transform.position, randomPos, speed * Time.deltaTime);
            movement = randomPos;
            timer = 3;
        }
        timer -= Time.deltaTime;

    }*/

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
            Debug.Log(lastPlayerPos);
        }
        else
        {
            trackPlayer = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            aggro = true;
            StartCoroutine(ShootLaser());
        }
    }

    private  void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            aggro = false;
            lastPlayerPos = FindObjectOfType<Player>().transform.position;
            movement = lastPlayerPos;
            trackPlayer = true;            
        }        
    }

    public virtual void OnDestroy()
    {
        Player player = FindObjectOfType<Player>();
        player.GetComponent<Player>().SetPlayerLockStateFalse();
    }
}
