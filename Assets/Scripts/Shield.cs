using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float shieldPoints = 100f;
    public float maxShieldPoints = 100f;
    
    float shieldTimer = 10f;

    bool isShieldActive = true;
    bool isShieldDamaged = false;

    [SerializeField] ShieldBar shieldBar;

    [SerializeField] PlayerStatusBars playerStatusBars;

    private void Start()
    {
        CheckAndSetShieldBar();
        CheckAndSetStatusBar();
    }

    private void Update()
    {
        ShieldCooldown();
    }

    public void DamageShield()
    {
        StopAllCoroutines();
        isShieldDamaged = true;
        shieldTimer = 10f;
        shieldPoints -= 20;
        CheckAndSetShieldBar();
        CheckAndSetStatusBar();
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
            shieldPoints += 10f;
            CheckAndSetShieldBar();
            CheckAndSetStatusBar();
            Debug.Log("Shield Recharging");
            yield return new WaitForSeconds(1);
        }
        Debug.Log("Shield Recharge Completed");       
    }

    public bool IsShieldActive()
    {
        return isShieldActive;
    }

    private void SetShieldActiveTrue()
    {
        isShieldActive = true;
        isShieldDamaged = false;
        Debug.Log("Shield Recharge Started");
        StartCoroutine(RechargeShield());       
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void SetShieldActiveFalse()
    {
        isShieldActive = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void CheckAndSetStatusBar()
    {
        if (playerStatusBars != null)
        {
            playerStatusBars.SetShieldBar(shieldPoints, maxShieldPoints);
        }
    }

    private void CheckAndSetShieldBar()
    {
        if(shieldBar != null)
        {
            shieldBar.SetShieldBar(shieldPoints, maxShieldPoints);
        }
    }
}
