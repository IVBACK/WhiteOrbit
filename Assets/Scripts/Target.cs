using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Vector3 targetPos;

    [SerializeField] bool isTargeted = false;
       
    public void Update()
    {
        UpdatePositon();
    }   

    public void OnMouseDown()
    {
        isTargeted = true;
        FindObjectOfType<Player>().SetPlayerLockStateTrue();
    }

    public void BreakLock()
    {
        isTargeted = false;
    }

    public Vector3 GetTargetPos()
    {
        return targetPos;
    }

    private void UpdatePositon()
    {
        if(isTargeted != true) { return; }
        targetPos = transform.position;
    }

    public bool ReturnTargetedState()
    {
        return isTargeted;
    }
}
