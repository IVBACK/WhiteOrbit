using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] float laserLifeTime = 0.6f;

    Vector3 targetPos;

    public virtual void ProjectileMove()
    {
        transform.Translate(Vector2.right * 1);
    }

    public virtual void DestroyLaserAfterSec()
    {
        Destroy(this.gameObject, laserLifeTime);
    }
}
