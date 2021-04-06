using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAlien : Npc
{

    void Update()
    {
        Aggro();
        TrackPlayer();
        RandomMovement();
    }

    public override void Aggro()
    {
        base.Aggro();
    }

    public override void TrackPlayer()
    {
        base.TrackPlayer();  
    }

    public override void RandomMovement()
    {
        base.RandomMovement();
    }
}
