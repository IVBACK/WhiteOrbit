using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{

    private bool isTargeted = false;
    private bool isLocked = false;
    public bool isTargetedByPlayer;

    [SerializeField] GameObject targetCross;

    public GameObject targetObject;

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
        isTargeted = true;
        isTargetedByPlayer = true;
        SetTargetCrossOn();
        Player player = FindObjectOfType<Player>();
        player.SetPlayerLockStateTrue();
        player.GetComponent<TargetSystem>().targetObject = this.gameObject;
    }      

    public void BreakPlayerLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isTargeted = false;
            FindObjectOfType<Player>().SetPlayerLockStateFalse();
            SetTargetCrossOff();          
        }
    }

     private void LockTarget()
    {
        if (isLocked != true) { return; }
        {          
            var relativePos = targetObject.transform.position - transform.position;
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

    public bool ReturnTargetedState()
    {
        return isTargeted;
    }

    public bool ReturnLockedState()
    {
        return isLocked;
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
