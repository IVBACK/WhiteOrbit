using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAlien : Npc
{
    void Update()
    {
        Rotate();
        Aggro();
        TrackTarget();
        RandomMovement();       
    }

    public override void Aggro()
    {
        base.Aggro();
    }

    public override void TrackTarget()
    {
        base.TrackTarget();
    }

    public override void RandomMovement()
    {
        base.RandomMovement();
    }

    public override void Rotate()
    {
        base.Rotate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<NpcAlien>()) { return; }
        if(collision.GetComponent<TargetSystem>())
        {
            aggro = true;
            patrol = false;
            GetComponent<TargetSystem>().SetLockStateTrue();
            targetGameObject = collision.gameObject;
            StartShoot();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<NpcAlien>()) { return; }
        if (collision.GetComponent<TargetSystem>())
        {
            aggro = false;
            collision.GetComponent<TargetSystem>().SetTargetedStateFalse();
            GetComponent<TargetSystem>().SetLockStateFalse();
            targetLastPos = collision.transform.position;
            movement = targetLastPos;
            trackPlayer = true;
        }
    }
}
