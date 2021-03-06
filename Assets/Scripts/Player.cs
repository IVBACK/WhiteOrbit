using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float shootDelay = 1f;

    [SerializeField] GameObject gun;
    [SerializeField] GameObject laser;
    [SerializeField] GameObject rocketlauncher;
    [SerializeField] GameObject rocket;

    [SerializeField] CooldownBars cooldownBars;

    private Vector3 mousePos;

    private Quaternion rotation;

    private TargetSystem playerTargetSystem;

    [HideInInspector] public bool isClicked = false;

    private bool isLocked;
    private bool isFiring;   
    

    private void Start()
    {
        playerTargetSystem = GetComponent<TargetSystem>();
    }

    void Update()
    {
        CheckIsClicked();
        Movement();
        StartShootingLaser();
        ShootRocket();
    }

    private void Movement()
    {
        if(isClicked != true) { return; }
        if (Input.GetMouseButton(0))
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

    private void StartShootingLaser()
    {
        if (isLocked != true) { return; }
        if (isFiring != false) { return; }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ShootLaser());
        }      
    }

    IEnumerator ShootLaser()
    {
        while(isLocked)
        {
            GameObject laserP = Instantiate(laser, gun.transform.position, Quaternion.identity) as GameObject;
            laserP.transform.rotation = rotation;
            isFiring = true;
            yield return new WaitForSeconds(shootDelay);
        }       
    }

    private void ShootRocket()
    {
        if (isLocked != true) { return; }
        if (Input.GetMouseButtonDown(1))
        {
            if(cooldownBars.rocketTimer != false) { return; }
            if(GetComponent<PlayerInventory>().UseRocket())
            {
                cooldownBars.StartRocketTimer();
                GameObject rocketP = Instantiate(rocket, gun.transform.position, Quaternion.identity) as GameObject;
                rocketP.transform.rotation = rotation;
            }           
        }
    }

    private void CheckIsClicked()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isClicked = true;
        }       
    }

    public void SetPlayerLockStateTrue()
    {
        isLocked = true;
        playerTargetSystem.SetLockStateTrue();
    }

    public void SetPlayerLockStateFalse()
    {
        isLocked = false;
        isFiring = false;
        playerTargetSystem.SetLockStateFalse();
    }
}
