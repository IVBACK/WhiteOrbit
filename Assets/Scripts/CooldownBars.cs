using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBars : MonoBehaviour
{
    [SerializeField] Image rocketCooldown;

    float timer = 10f;
    float maxTimer = 10f;

    public bool rocketTimer = false;

    private void Start()
    {
        rocketCooldown.fillAmount = 0;
    }

    void Update()
    {
        RocketTimer();
    }

    private void RocketTimer()
    {
        if(rocketTimer != true) { return; }
        timer -= Time.deltaTime;
        rocketCooldown.fillAmount = timer / maxTimer;
        if (timer <= 0)
        {
            timer = maxTimer;
            rocketTimer = false;
        }
    }

    public void StartRocketTimer()
    {
        rocketTimer = true;
    }
}
