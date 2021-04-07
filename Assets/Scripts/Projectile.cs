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

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        GameObject target = otherCollider.gameObject;
        if(target.GetComponent<Player>())
        {
            CheckPlayerShield(target);
        }
        else
        {
            if(target.GetComponentInChildren<Shield>() != null)
            {
                CheckNpcShield(target);
            }
            else
            {
                DamageHealth(target);
            }            
        }
    }

    private void CheckNpcShield(GameObject target)
    {

        if (target.GetComponentInChildren<Shield>().IsShieldActive())
        {
            DamageShield(target);
        }
        else
        {
            DamageHealth(target);
        }
    }

    private void CheckPlayerShield(GameObject target)
    {
        if (target.GetComponentInChildren<Shield>().IsShieldActive())
        {
            DamageShield(target);
        }
        else
        {
            DamageHealth(target);
        }
    }

    private void DamageShield(GameObject target)
    {
        Debug.Log("DAMAGE SHIELD");
        target.GetComponentInChildren<Shield>().DamageShield();
        Destroy(gameObject);
    }

    private void DamageHealth(GameObject target)
    {
        Debug.Log("DAMAGE HEALTH");
        target.GetComponent<Health>().DamageHealth();
        Destroy(gameObject);
    }
}
