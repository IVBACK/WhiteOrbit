using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;
    public int Maxhealth = 100;

    public HealthBar healthbar;

    private void Start()
    {
        healthbar.SetHealthBar(health, Maxhealth);
    }
    
    private void HandleDead()
    {
        Destroy(gameObject);
    }

    public void DamageHealth()
    {
        health -= 20;
        healthbar.SetHealthBar(health, Maxhealth);
        if (health <= 0)
        {
            HandleDead();
        }
    }
}
