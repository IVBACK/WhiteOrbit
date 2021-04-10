using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float shieldPoints = 100f;
    public float maxShieldPoints = 100f;
    [SerializeField] float shieldTimer = 10f;

    [SerializeField] bool isShieldActive = true;

    [SerializeField] bool isShieldDamaged = false;

    public ShieldBar shieldBar;

    private void Start()
    {
        shieldBar.SetShieldBar(shieldPoints, maxShieldPoints);
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
        shieldBar.SetShieldBar(shieldPoints, maxShieldPoints);
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
            shieldBar.SetShieldBar(shieldPoints, maxShieldPoints);
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
}
