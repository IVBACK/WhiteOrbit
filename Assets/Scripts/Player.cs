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

    Quaternion rotation;

    bool isLocked;
 
    void Update()
    {
        Movement();
        StartShooting();
        //UpdateLockState();
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
        
        Player[] players = FindObjectsOfType<Player>();
        foreach (Player player in players)
        {
            if (player.GetComponent<Player>())
            {
                player.GetComponent<Locking>().SetLockStateTrue();
            }
        }
    }

    public void UpdateLockState()
    {
        isLocked = false;
    }  
}
