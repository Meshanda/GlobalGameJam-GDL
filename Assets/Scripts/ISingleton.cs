using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISingleton<T> : MonoBehaviour where T : ISingleton<T>
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
