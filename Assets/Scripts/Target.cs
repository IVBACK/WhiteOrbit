using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Vector3 targetPos;
    Vector2 movement;

    [SerializeField] float speed = 1f;
    float timer = 1;

    [SerializeField] bool isTargeted = false;
    
    

    public void Update()
    {
        RandomMovement();
        UpdatePositon();
    }

    private void RandomMovement()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            movement = new Vector2 (Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            transform.position = Vector3.MoveTowards(transform.position, movement, speed * Time.deltaTime);
            timer = 1;         
        }      
    }

    public void OnMouseDown()
    {
        isTargeted = true;
        FindObjectOfType<Player>().SetLockStateTrue();
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
