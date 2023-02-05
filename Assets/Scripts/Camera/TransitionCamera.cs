using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public enum RoomType
{
    DoubleJump,
    Dash,
    Grappin,
    WallJump,
    Hache
}
public class TransitionCamera : MonoBehaviour
{
    [SerializeField] private bool _useCollision = true;
    [SerializeField] private RoomType _roomType;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_useCollision)
            Transition();
    }

    public void Transition()
    {
        PlayerMovement.Instance.Movable = false;

        var camScript = CameraManager.Instance;
        switch (_roomType)
        {
            case RoomType.DoubleJump:
                camScript.TransitionRoomDoubleJump();
                break;
            case RoomType.Dash:
                camScript.TransitionRoomDash();
                break;
            case RoomType.Grappin:
                camScript.TransitionRoomGrappin();
                break;
            case RoomType.WallJump:
                camScript.TransitionRoomWallJump();
                break;
            case RoomType.Hache:
                camScript.TransitionRoomHache();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        StartCoroutine(MoveAgainRoutine());
    }

    private IEnumerator MoveAgainRoutine()
    {
        yield return new WaitForSeconds(CameraManager.Instance.BlendTime);
        
        PlayerMovement.Instance.Movable = true;
    }
}
