using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcProjectile : Projectile
{
    void FixedUpdate()
    {
        ProjectileMove();
        DestroyProjectileAfterSec();
    }

    public override void ProjectileMove()
    {
        base.ProjectileMove();
    }

    public override void DestroyProjectileAfterSec()
    {
        base.DestroyProjectileAfterSec();
    }
}
