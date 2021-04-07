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

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        GameObject target = otherCollider.gameObject;
        if (target.GetComponent<Player>())
        {
            Debug.Log("HIT");
            target.GetComponent<Health>().TakeDamage();
            Destroy(gameObject);
        }
    }
}
