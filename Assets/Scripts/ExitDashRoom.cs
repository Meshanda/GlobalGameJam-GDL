using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDashRoom : MonoBehaviour
{
    private bool _isExiting = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isExiting)
        {
            PlayerAnimation.Instance.PlayExitDashAnimation();
        }

        _isExiting = !_isExiting;
    }
}
