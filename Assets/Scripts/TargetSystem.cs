using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    Vector3 targetPos;

    [SerializeField] bool isTargeted = false;
    [SerializeField] bool isLocked = false;

    private void Update()
    {
        BreakPlayerLock();
        LockTarget();
    }

    private void OnMouseDown()
    {
        SetTargetedStateTrue();
        FindObjectOfType<Player>().SetPlayerLockStateTrue();
        GetComponent<Npc>().SetTargetCrossOn();
    } 

    public void BreakPlayerLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetTargetedStateFalse();
            FindObjectOfType<Player>().SetPlayerLockStateFalse();
            GetComponent<Npc>().SetTargetCrossOff();
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
                    targetPos = targetsystem.transform.position;
                }
            }
            Debug.Log(targetPos);

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
}
