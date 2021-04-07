using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] int shieldPoint = 100;
    [SerializeField] float shieldTimer = 10f;

    [SerializeField] bool isShieldActive = true;

    [SerializeField] bool isShieldDamaged = false;

    private void Update()
    {
        ShieldCooldown();
    }

    public void DamageShield()
    {
        isShieldDamaged = true;
        shieldTimer = 10f;
        shieldPoint -= 20;
        if (shieldPoint <= 0)
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

    public bool IsShieldActive()
    {
        return isShieldActive;
    }

    private void SetShieldActiveTrue()
    {
        isShieldActive = true;
        isShieldDamaged = false;
        shieldPoint = 100;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void SetShieldActiveFalse()
    {
        isShieldActive = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
