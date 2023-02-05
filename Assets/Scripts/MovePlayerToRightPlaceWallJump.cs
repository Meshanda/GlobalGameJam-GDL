using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerToRightPlaceWallJump : MonoBehaviour
{
    [SerializeField] private Transform _rightPlace;

    private bool _isEntering = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isEntering)
        {
            // Move player to the right place
            // PlayerMovement.Instance.gameObject.transform.position = _rightPlace.position;

            PlayerAnimation.Instance.PlayEnterWallJumpAnimation();
        }

        _isEntering = !_isEntering;
    }
}
