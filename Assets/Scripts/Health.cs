using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
        
    [SerializeField] GameObject explosion;

    [SerializeField] HealthBar healthbar;

    private void Start()
    {
        SetHealthBar();
    }

    private void HandleDead()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public virtual void DamageHealth(int damage)
    {
        health -= damage;
        SetHealthBar();
        if (health <= 0)
        {
            HandleDead();
        }
    }

    public void SetHealthBar()
    {
        healthbar.UpdateHeatlhBar(health, maxHealth);
    }
}
