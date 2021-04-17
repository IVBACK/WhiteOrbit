using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileLifeTime;
    [SerializeField] int damage;

    public virtual void ProjectileMove()
    {
        transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
    }

    public virtual void DestroyProjectileAfterSec()
    {
        Destroy(this.gameObject, projectileLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        GameObject target = otherCollider.gameObject;
        Shield targetShield = target.GetComponent<Shield>();
        Health targetHealth = target.GetComponent<Health>();
        if (targetShield != null && targetShield.IsShieldActive())
        {
            targetShield.DamageShield(damage);
            Destroy(gameObject);
        }
        else if(targetHealth != null)
        {
            targetHealth.DamageHealth(damage);
            Destroy(gameObject);
        }
    }   
}
