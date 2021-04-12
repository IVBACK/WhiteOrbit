using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
        
    [SerializeField] GameObject explosion;

    [SerializeField] HealthBar healthbar;

    [SerializeField] PlayerStatusBars playerStatusBars;

    private void Start()
    {
        CheckAndSetHealthBar();
        CheckAndSetStatusBar();
    }

    private void HandleDead()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public virtual void DamageHealth()
    {
        health -= 20;
        CheckAndSetHealthBar();
        if (health <= 0)
        {
            HandleDead();
        }
    }

    private void CheckAndSetStatusBar()
    {
        if(playerStatusBars != null)
        {
            playerStatusBars.SetHealthBar(health, maxHealth);
        }      
    }

    private void CheckAndSetHealthBar()
    {
        if (healthbar != null)
        {
            healthbar.SetHealthBar(health, maxHealth);
        }
        else
        {
            playerStatusBars.SetHealthBar(health, maxHealth);
        }
    }
}
