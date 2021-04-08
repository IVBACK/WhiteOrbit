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
        Shield targetShield = target.GetComponentInChildren<Shield>();
        Health targetHealth = target.GetComponent<Health>();
        if ( targetShield != null && targetShield.IsShieldActive())
        {
            Debug.Log("DAMAGE SHIELD");
            targetShield.DamageShield();
            Destroy(gameObject);
        }
        else if(targetHealth != null)
        {
            Debug.Log("DAMAGE HEALTH");
            targetHealth.DamageHealth();
            Destroy(gameObject);
        }
    }   
}
