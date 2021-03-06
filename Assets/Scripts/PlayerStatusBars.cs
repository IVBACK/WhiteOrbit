using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusBars : MonoBehaviour
{

    [SerializeField] Image expBar;
    [SerializeField] Image healthBar;
    [SerializeField] Image shieldBar;
    [SerializeField] Text levelText;

    public Vector3 offset;

    public void SetExpBar(float exp, float maxExp)
    {
        expBar.fillAmount = exp / maxExp;
    }

    public void SetHealthBar(float health, float maxHealth)
    {
        healthBar.fillAmount = health / maxHealth;
    }

    public void SetShieldBar(float shieldPoint, float maxShieldPoints)
    {
        shieldBar.fillAmount = shieldPoint / maxShieldPoints;
    }

    private void Update()
    {
        expBar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
        shieldBar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
