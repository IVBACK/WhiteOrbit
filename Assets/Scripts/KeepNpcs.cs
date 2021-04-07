using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepNpcs : MonoBehaviour
{
    int keepNpcs;
    
    private void Awake()
    {
        keepNpcs = FindObjectsOfType<KeepNpcs>().Length;
        if(keepNpcs >= 0)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
