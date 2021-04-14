using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        transform.rotation = player.transform.rotation;
    }

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
