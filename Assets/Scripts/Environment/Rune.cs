using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using ScriptableObjects.Variables;
using UnityEngine;

public class Rune : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsToDisable;
    [SerializeField] private List<GameObject> _objectsToEnable;
    [SerializeField] private BoolVariable _newPower;
    [SerializeField] private RoomType _runeType;

    private void Awake()
    {
        _newPower.value = false;
        
        if (_newPower.value)
            Destroy(gameObject);
    }

    private void UnlockPower()
    {
        _newPower.value = true;
        foreach (var go in _objectsToDisable.Where(go => go))
            go.SetActive(false);
        foreach (var go in _objectsToEnable.Where(go => go))
            go.SetActive(true);

        AudioManager.Instance.Play("rune");
        OverlayManager.RefreshOverlay();
        PopUp();
    }

    private void PopUp()
    {
        
        
        PopUpScript.Instance.OnTogglePopUp(_runeType);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        UnlockPower();
        Destroy(gameObject);
    }
}

