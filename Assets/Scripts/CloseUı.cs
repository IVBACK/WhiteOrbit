using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseU� : MonoBehaviour
{
    [SerializeField] GameObject U�;

    private void Start()
    {
        U�.SetActive(false);
    }

    public void CloseShop()
    {
        U�.SetActive(false);
        Time.timeScale = 1;
    }
}
