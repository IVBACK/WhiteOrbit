using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    Vector3 targetPos;

    TargetSystem target;

    private bool isTargeted = false;
    private bool isLocked = false;

    [SerializeField] GameObject targetCross;

    private void Awake()
    {
        SetTargetCrossOff();
    }

    private void Update()
    {
        BreakPlayerLock();
        LockTarget();
    }

    private void OnMouseDown()
    {
        SetTargetedStateTrue();
        FindObjectOfType<Player>().SetPlayerLockStateTrue();
        SetTargetCrossOn();
    }      

    public void BreakPlayerLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetTargetedStateFalse();
            FindObjectOfType<Player>().SetPlayerLockStateFalse();
            SetTargetCrossOff();          
        }
    }

    private void LockTarget()
    {
        if (isLocked != true) { return; }
        {
            TargetSystem[] targetSystems = FindObjectsOfType<TargetSystem>();
            foreach (TargetSystem targetsystem in targetSystems)
            {
                if (targetsystem.ReturnTargetedState())
                {
                    target = targetsystem;
                    targetPos = targetsystem.transform.position;
                }
            }

            var relativePos = targetPos - transform.position;
            var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            var toTargetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = toTargetRotation;
        }
    }

    public void SetTargetedStateFalse()
    {
        isTargeted = false;
    }

    public void SetTargetedStateTrue()
    {
        isTargeted = true;
    }

    public void SetLockStateFalse()
    {
        isLocked = false;
    }

    public void SetLockStateTrue()
    {
        isLocked = true;
    }

    public Vector3 ReturnCurrentTargetPos()
    {
        return targetPos;
    }

    public bool ReturnTargetedState()
    {
        return isTargeted;
    }

    public void SetTargetCrossOn()
    {
        targetCross.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void SetTargetCrossOff()
    {
        targetCross.GetComponent<SpriteRenderer>().enabled = false;
    }
}
