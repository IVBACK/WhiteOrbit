using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    [SerializeField] GameObject targetCross;
    [HideInInspector] public GameObject targetObject;

    [HideInInspector] public bool isTargetedByPlayer;
 
    private bool isLocked = false;  

    [HideInInspector] public List<GameObject> targets = new List<GameObject>();

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
            if(isTargetedByPlayer)
            {
                FindObjectOfType<Player>().SetPlayerLockStateFalse();
                isTargetedByPlayer = false;
                SetTargetCrossOff();
            }                   
        }
    }

    private void LockTarget()
    {
        if (isLocked != true) { return; }
        {
            if(targetObject != null)
            {
                var relativePos = targetObject.transform.position - transform.position;
                var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
                var toTargetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = toTargetRotation;
            }          
        }
    }

    public void SetLockStateFalse()
    {
        isLocked = false;
    }

    public void SetLockStateTrue()
    {
        isLocked = true;
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
