using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Variables;
using UnityEngine;

public class OverlayManager : MonoBehaviour
{
    // Event
    public static Action RefreshOverlay;
    
    // Double Jump
    [SerializeField] private GameObject _doubleJumpImg;
    [SerializeField] private BoolVariable _doubleJumpUnlocked;
    
    // Dash
    [SerializeField] private GameObject _dashImg;
    [SerializeField] private BoolVariable _dashUnlocked;
    
    // Grappin
    [SerializeField] private GameObject _grappinImg;
    [SerializeField] private BoolVariable _grappinUnlocked;
   
    // Wall Jump
    [SerializeField] private GameObject _wallJumpImg;
    [SerializeField] private BoolVariable _wallJumpUnlocked;

    private void OnEnable()
    {
        RefreshOverlay += OnRefreshOverlay;
    }

    private void OnDisable()
    {
        RefreshOverlay -= OnRefreshOverlay;
    }

    private void Awake()
    {
        OnRefreshOverlay();
    }

    private void OnRefreshOverlay()
    {
        if (_doubleJumpUnlocked.value)
            _doubleJumpImg.SetActive(true);
        if (_dashUnlocked.value)
            _dashImg.SetActive(true);
        if (_grappinUnlocked.value)
            _grappinImg.SetActive(true);
        if (_wallJumpUnlocked.value)
            _wallJumpImg.SetActive(true);
    }
}
