using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    Vector3 targetPos;

    GameObject target;

    bool isTargetPicked = false;

    [SerializeField] GameObject explosion;

    void Update()
    {
        LockTarget();
        CheckTarget();
        GuideToTarget();
        DestroyProjectileAfterSec();
    }   

    private void LockTarget()
    {
        if(isTargetPicked != false) { return; }
        TargetSystem[] targetSystems = FindObjectsOfType<TargetSystem>();
        foreach (TargetSystem targetSystem in targetSystems)
        {
            if (targetSystem.ReturnTargetedState())
            {
                target = targetSystem.gameObject;
                isTargetPicked = true;
            }           
        }
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
