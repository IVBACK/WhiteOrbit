using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] float laserLifeTime = 0.6f;

    Vector3 targetPos;

    void FixedUpdate()
    {
        GuideToTarget();         
        DestroyLaserAfterSec();
    }

    private void GuideToTarget()
    {
        targetPos = FindObjectOfType<Player>().transform.position;
            
        transform.position = Vector3.MoveTowards(transform.position, targetPos, projectileSpeed * Time.deltaTime);

        var relativePos = targetPos - transform.position;
        var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        var toTargetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = toTargetRotation;
    }
    
    private void DestroyLaserAfterSec()
    {
        Destroy(this.gameObject, laserLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        GameObject target = otherCollider.gameObject;
        if (!target.GetComponent<Npc>())
        {
            if (target.GetComponent<Target>())
            {
                Debug.Log("HIT");
                target.GetComponent<Health>().TakeDamage();
                Destroy(gameObject);
            }
        }
    }
}
