using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int shieldPoints = 100;
    public int maxShieldPoints = 100;
    
    float shieldTimer = 10f;

    bool isShieldActive = true;
    bool isShieldDamaged = false;

    [SerializeField] ShieldBar shieldBar;

    [SerializeField] GameObject shield;

    private void Start()
    {
        SetShieldBar();
    }

    private void Update()
    {
        ShieldCooldown();
    }

    public void DamageShield(int damage)
    {
        StopAllCoroutines();
        isShieldDamaged = true;
        shieldTimer = 10f;
        shieldPoints -= damage;
        SetShieldBar();
        if (shieldPoints <= 0)
        {
            SetShieldActiveFalse();
        }
    }

    private void ShieldCooldown()
    {
        if(isShieldDamaged != true) { return; }
        shieldTimer -= Time.deltaTime;
        if(shieldTimer <= 0)
        {
            SetShieldActiveTrue();
        }
    }

    private IEnumerator RechargeShield()
    {
        while (shieldPoints < maxShieldPoints)
        {
            shieldPoints += 10;
            SetShieldBar(); ;
            yield return new WaitForSeconds(1);
        }     
    }

    public bool IsShieldActive()
    {
        return isShieldActive;
    }

    private void SetShieldActiveTrue()
    {
        isShieldActive = true;
        isShieldDamaged = false;
        StartCoroutine(RechargeShield());       
        shield.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void SetShieldActiveFalse()
    {
        isShieldActive = false;
        shield.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void SetShieldBar()
    {
        shieldBar.UpdateShieldBar(shieldPoints, maxShieldPoints);
    }
}
