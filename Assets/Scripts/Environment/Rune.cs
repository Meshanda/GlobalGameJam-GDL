using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using ScriptableObjects.Variables;
using UnityEngine;

public class Rune : MonoBehaviour
{
    [SerializeField] private BoolVariable _newPower;

    private void UnlockPower()
    {
        _newPower.value = true;

        OverlayManager.RefreshOverlay();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        UnlockPower();
    }
}

