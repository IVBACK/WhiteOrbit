using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float shootDelay = 1f;

    [SerializeField] GameObject[] guns;
    [SerializeField] GameObject laser;
    [SerializeField] GameObject gun;  //Placeholder

    Vector3 mousePos;
    Vector3 targetPos;

    Quaternion rotation;

    bool isLocked = false;
 
    void Update()
    {
        Movement();
        StartShooting();
        LockTarget();
        BreakLock();
    }

    private void Movement()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Rotate();           
        }
        transform.position = Vector3.MoveTowards(transform.position, mousePos, speed * Time.deltaTime);      
    }

    private void Rotate()
    {
        if (isLocked == false)
        {
            var relativePos = mousePos - transform.position;
            var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
        }       
    }

    private void StartShooting()
    {
        if (isLocked != true) { return; }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ShootLaser());
        }      
    }

    IEnumerator ShootLaser()
    {
        while(isLocked)
        {
            yield return new WaitForSeconds(shootDelay);
            GameObject laserP = Instantiate(laser, gun.transform.position, Quaternion.identity) as GameObject;
            laserP.transform.rotation = rotation;
        }       
    }

    public void SetLockStateTrue()
    {
        isLocked = true;       
    }

    private void BreakLock()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isLocked = false;
            Target[] targets = FindObjectsOfType<Target>();
            foreach(Target target in targets)
            {
                target.BreakLock();
            }
        }       
    }

    private void LockTarget()
    {
        if(isLocked != true) { return; }
        {
            Target[] targetsPos = FindObjectsOfType<Target>();
            foreach(Target target in targetsPos)
            {
                if(target.ReturnTargetedState())
                {
                    targetPos = target.GetTargetPos();
                }
            }
            Debug.Log(targetPos);

            var relativePos = targetPos - transform.position;
            var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            var toTargetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation =  toTargetRotation;
        }
    }

    public Vector3 ReturnCurrentTargetPos()
    {
        return targetPos;
    }
}
