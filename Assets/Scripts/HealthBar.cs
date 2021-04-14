using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Color low;
    [SerializeField] Color high;
    [SerializeField] Vector3 offset;

    Health healthComp;

    [SerializeField] bool alwaysOn;

    private void Awake()
    {
        healthComp = GetComponentInParent<Health>();
    }

    private void Start()
    {
        healthComp.SetHealthBar();
    }

    public void UpdateHeatlhBar(int health, int maxHealth)
    {
        slider.gameObject.SetActive(CheckAlwaysOn());
        slider.value = health;
        slider.maxValue = maxHealth;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }

    private void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }

    private bool CheckAlwaysOn()
    {
        if(alwaysOn == true)
        {
            return true;
        }
        else
        {
            return healthComp.health < healthComp.maxHealth;
        }
    }
}
