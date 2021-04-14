using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Color low;
    [SerializeField] Color high;
    [SerializeField] Vector3 offset;

    Shield shieldComp;

    [SerializeField] bool alwaysOn;

    private void Awake()
    {
        shieldComp = GetComponentInParent<Shield>();
    }

    private void Start()
    {
        shieldComp.SetShieldBar();
    }

    public void UpdateShieldBar(int shieldPoints, int maxShieldPoints)
    {
        slider.gameObject.SetActive(CheckAlwaysOn());
        slider.value = shieldPoints;
        slider.maxValue = maxShieldPoints;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }

    private void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }

    private bool CheckAlwaysOn()
    {
        if (alwaysOn == true)
        {
            return true;
        }
        else
        {
            return shieldComp.shieldPoints < shieldComp.maxShieldPoints;
        }
    }
}
