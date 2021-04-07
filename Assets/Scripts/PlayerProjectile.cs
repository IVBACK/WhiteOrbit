using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void FixedUpdate()
    {
        PlayerProjectileMove();
        DestroyLaserAfterSec();
    }

    private void PlayerProjectileMove()
    {
        transform.rotation = player.transform.rotation;
        transform.Translate(Vector2.right * 1);
    }

    public override void DestroyLaserAfterSec()
    {
        base.DestroyLaserAfterSec();
    }
}
