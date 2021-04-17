using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerInventory : MonoBehaviour
{
    public int rocketCount;
    [SerializeField] Text rocketCountText;

    private void Start()
    {
        rocketCountText.text = rocketCount.ToString();
    }

    public void AddRocket()
    {
        rocketCount++;
        rocketCountText.text = rocketCount.ToString();
        Debug.Log("+1 rocket");
    }

    public bool UseRocket()
    {
        if (rocketCount > 0)
        {
            rocketCount--;
            rocketCountText.text = rocketCount.ToString();
            return true;
        }
        else
        {
            return false;
        }
    }
}
