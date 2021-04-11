using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] int level = 1;
    [SerializeField] float totalExp = 0;
    [SerializeField] float maxExp = 100;

    public ExpBar expBar;

    public Text expText; 

    void Start()
    {
        expBar.SetExpBar(totalExp, maxExp);
        expText.text = level.ToString();
    }

    public void GetExp(int exp)
    {
        totalExp += exp;
        expBar.SetExpBar(totalExp, maxExp);
        if(totalExp >= maxExp)
        {
            level += 1;
            expText.text = level.ToString();
            totalExp = 0;
            maxExp += 50;
            expBar.SetExpBar(totalExp, maxExp);
        }
    }
}
