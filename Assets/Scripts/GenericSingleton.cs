using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : GenericSingleton<T>
{
    public static T Instance { get; private set; }
    protected virtual bool DoDestroyOnLoad { get; set; } = true;

    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = (T) this;
            if (!DoDestroyOnLoad)
            {
                DontDestroyOnLoad(this);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
