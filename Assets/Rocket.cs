using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Vector3 targetPos;

    GameObject target;

    bool isTargetPicked = false;

    [SerializeField] float speed;

    [SerializeField] GameObject explosion;

    void Update()
    {
        LockTarget();
        GuideToTarget();       
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

        MoveToTarget();
    }

    private void MoveToTarget()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        GameObject target = otherCollider.gameObject;
        Shield targetShield = target.GetComponentInChildren<Shield>();
        Health targetHealth = target.GetComponent<Health>();
        if (targetShield != null && targetShield.IsShieldActive())
        {
            targetShield.DamageShield();
            Destroy(gameObject);
        }
        else if (targetHealth != null)
        {
            targetHealth.DamageHealth();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
