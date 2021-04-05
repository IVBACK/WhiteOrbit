using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    
    Vector3 movement;

    CircleCollider2D agroCollider;

    [SerializeField] float speed = 1f;    
    [SerializeField] float timer = 1;

    [SerializeField] bool aggro = false;

    void Update()
    {
        RandomMovement();
        AggroMovement();
    }

    private void RandomMovement()
    {
        if(aggro != false) { return; }
        {
             movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
             transform.position = Vector3.MoveTowards(transform.position, movement, speed * Time.deltaTime);
             Rotate();          
        }      
    }

    private void AggroMovement()
    {
        if(aggro != true) { return; }
        {
            Vector3 target = FindObjectOfType<Player>().transform.position;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            movement = target;
            Rotate();
        }
    }

    private void Rotate()
    {
        var relativePos = movement - transform.position;
        var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        var toTargetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = toTargetRotation;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        aggro = true;
    }
}
