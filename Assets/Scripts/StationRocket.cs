using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationRocket : Projectile
{
    [SerializeField] GameObject explosion;
    
    private Vector3 targetPos;

    private GameObject target;

    private bool isTargetPicked = false;

    void Update()
    {
        LockTarget();
        CheckTarget();
        GuideToTarget();
        DestroyProjectileAfterSec();
    }   

    private void LockTarget()
    {
        if (isTargetPicked != false) { return; }
        target = GetComponentInParent<TargetSystem>().targetObject;
        isTargetPicked = true;
    }

    private void GuideToTarget()
    {
        if(isTargetPicked != true) { return; }
        targetPos = target.transform.position;

        var relativePos = targetPos - transform.position;
        var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        Quaternion toTargetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = toTargetRotation;

        ProjectileMove();
    }

    private void CheckTarget()
    {
        if(target == null)
        {
            isTargetPicked = false;
            Destroy(gameObject);
        }
    }

    public override void ProjectileMove()
    {
        base.ProjectileMove();
    }

    public override void DestroyProjectileAfterSec()
    {
        base.DestroyProjectileAfterSec();
    }

    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
