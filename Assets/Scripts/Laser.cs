using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float projectileSpeed;

    Vector3 targetPos;

    void Update()
    {
        GuideToTarget();
    }

    private void GuideToTarget()
    {
        targetPos = FindObjectOfType<Player>().ReturnCurrentTargetPos();

        transform.position = Vector3.MoveTowards(transform.position, targetPos, projectileSpeed * Time.deltaTime);

        var relativePos = targetPos - transform.position;
        var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        var toTargetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = toTargetRotation;
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        GameObject target = otherCollider.gameObject;
        if (target.GetComponent<Target>())
        {
            Debug.Log("HIT");
            Destroy(gameObject);
        }
    }
}
