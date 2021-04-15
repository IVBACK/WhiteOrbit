using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUý : MonoBehaviour
{
    [SerializeField] GameObject Uý;

    private void Start()
    {
        Uý.SetActive(false);
    }

    public void CloseShop()
    {
        Uý.SetActive(false);
        Time.timeScale = 1;
    }
}
