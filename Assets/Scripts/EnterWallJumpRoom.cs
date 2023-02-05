using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterWallJumpRoom : MonoBehaviour
{
    private bool _isEntering = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isEntering)
        {
            PlayerAnimation.Instance.PlayEnterWallJumpAnimation();
        }

        _isEntering = !_isEntering;
    }
}
