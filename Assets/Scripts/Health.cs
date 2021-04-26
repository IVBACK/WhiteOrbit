using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
        
    [SerializeField] GameObject explosion;

    [SerializeField] GameObject destroyedState;

    [SerializeField] HealthBar healthbar;

    private void Start()
    {
        SetHealthBar();
    }

    private void HandleDead()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        if(destroyedState != null)
        {
            Instantiate(destroyedState, transform.position, Quaternion.identity);
        }
        if(GetComponent<Player>() == true)
        {
            FindObjectOfType<GameManager>().HandlePlayerDeath();
        }
        else
        {
            Destroy(gameObject);
        }        
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        SetHealthBar();
    }

    public void DamageHealth(int damage)
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
