using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locking : MonoBehaviour
{

    Vector3 targetPos;

    bool isLocked = false;

    void Update()
    {
        LockTarget();
        BreakLock();
    }

    public void BreakLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isLocked = false;
            Target[] targets = FindObjectsOfType<Target>();
            foreach (Target target in targets)
            {
                target.BreakLock();
                FindObjectOfType<Player>().SetPlayerLockStateFalse();
            }
        }
    }

    private void LockTarget()
    {
        if (isLocked != true) { return; }
        {
            Target[] targetsPos = FindObjectsOfType<Target>();
            foreach (Target target in targetsPos)
            {
                if (target.ReturnTargetedState())
                {
                    targetPos = target.GetTargetPos();
                }
            }
            Debug.Log(targetPos);

            var relativePos = targetPos - transform.position;
            var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            var toTargetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = toTargetRotation;
        }
    }

    public void SetLockStateTrue()
    {
        isLocked = true;
    }

    public void SetLockStateFalse()
    {
        isLocked = false;
    }

    public Vector3 ReturnCurrentTargetPos()
    {
        return targetPos;
    }

    public bool ReturnLockState()
    {
        return isLocked;
    }
}
