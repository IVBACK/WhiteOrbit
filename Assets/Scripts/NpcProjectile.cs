using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcProjectile : Projectile
{
    void FixedUpdate()
    {
        ProjectileMove();
        DestroyLaserAfterSec();
    }

    public override void ProjectileMove()
    {
        base.ProjectileMove();
    }

    public override void DestroyLaserAfterSec()
    {
        base.DestroyLaserAfterSec();
    }
}
