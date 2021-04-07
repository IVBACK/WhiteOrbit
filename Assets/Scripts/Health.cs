using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;
    
    private void HandleDead()
    {
        Destroy(gameObject);
    }

    public void DamageHealth()
    {
        health -= 20;
        if(health <= 0)
        {
            HandleDead();
        }
    }
}
