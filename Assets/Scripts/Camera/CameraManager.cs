using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : GenericSingleton<CameraManager>
{
    [SerializeField] private GameObject _followCam;
    [SerializeField] private GameObject _doubleJumpCam;
    [SerializeField] private GameObject _dashCam;
    [SerializeField] private GameObject _grappinCam;
    [SerializeField] private GameObject _wallJumpCam;
    [SerializeField] private GameObject _hacheCam;
    
    
    public float BlendTime = .7f;

    public void TransitionRoomDoubleJump()
    {
        if (_followCam.activeSelf)
        {
            _doubleJumpCam.SetActive(true);
            _followCam.SetActive(false);
        }
        else if (_doubleJumpCam.activeSelf)
        {
            _doubleJumpCam.SetActive(false);
            _followCam.SetActive(true);
        }
    }
    
    public void TransitionRoomDash()
    {
        if (_followCam.activeSelf)
        {
            _dashCam.SetActive(true);
            _followCam.SetActive(false);
        }
        else if (_dashCam.activeSelf)
        {
            _dashCam.SetActive(false);
            _followCam.SetActive(true);
        }
    }
    
    public void TransitionRoomGrappin()
    {
        if (_followCam.activeSelf)
        {
            _grappinCam.SetActive(true);
            _followCam.SetActive(false);
        }
        else if (_grappinCam.activeSelf)
        {
            _grappinCam.SetActive(false);
            _followCam.SetActive(true);
        }
    }
    
    public void TransitionRoomWallJump()
    {
        if (_followCam.activeSelf)
        {
            _wallJumpCam.SetActive(true);
            _followCam.SetActive(false);
        }
        else if (_wallJumpCam.activeSelf)
        {
            _wallJumpCam.SetActive(false);
            _followCam.SetActive(true);
        }
    }
    
    public void TransitionRoomHache()
    {
        if (_followCam.activeSelf)
        {
            _hacheCam.SetActive(true);
            _followCam.SetActive(false);
        }
        else if (_hacheCam.activeSelf)
        {
            _hacheCam.SetActive(false);
            _followCam.SetActive(true);
        }
    }
}
