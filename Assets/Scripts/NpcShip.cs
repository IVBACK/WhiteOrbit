using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcShip : Npc
{
    
    void Update()
    {
        Rotate();
        Aggro();
        //TrackTarget(); Currently disabled.
        RandomMovement();
        HandleTargeting();
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

    public override void HandleTargeting()
    {
        base.HandleTargeting();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<NpcAlien>()/*collision.GetComponent<NpcShip>() || collision.GetComponent<Player>() || collision.GetComponent<Station>()*/) { return; }
        if (collision.GetComponent<TargetSystem>())
        {
            targetSystem.targets.Add(collision.gameObject);
            aggro = true;
            targetCycle = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.GetComponent<NpcAlien>()/*collision.GetComponent<NpcShip>() || collision.GetComponent<Player>() || collision.GetComponent<Station>()*/) { return; }
        targetSystem.targets.Remove(collision.gameObject);

        targetCycle = true;
    }  
}
