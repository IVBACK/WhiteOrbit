using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcShip : Npc
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

    public override void Rotate()
    {
        base.Rotate();
    }

    public override void RandomMovement()
    {
        base.RandomMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<NpcShip>() || targetSystem.ReturnLockedState()) { return; }
        if (collision.GetComponent<TargetSystem>())
        {
            trackTarget = false;
            aggro = true;
            patrol = false;
            targetSystem.SetLockStateTrue();
            targetSystem.targetObject = collision.gameObject;
            StartPickPosOffset();
            StartShoot();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<NpcShip>()) { return; }

        if (collision.gameObject == targetSystem.targetObject) //When target is dead
        {
            aggro = false;           
            targetLastPos = collision.transform.position;
            movement = targetLastPos;
            trackTarget = true;
            targetSystem.SetLockStateFalse();
        }       

        else if (collision.GetComponent<TargetSystem>())
        {
            if (targetSystem.ReturnLockedState()) { return; }
            aggro = false;
            collision.GetComponent<TargetSystem>().SetTargetedStateFalse();
            targetSystem.SetLockStateFalse();
            targetLastPos = collision.transform.position;
            movement = targetLastPos;
            trackTarget = true;
        }
    }   
}
