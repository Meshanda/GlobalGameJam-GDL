using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _transformToFollow;

    [Header("Boundaries")]
    [SerializeField] private float _maxRight;
    [SerializeField] private float _maxLeft = 0f;

    private float _zOffset = -10f;

    private void Update()
    {
        float x = _transformToFollow.position.x;

        if (x < _maxLeft) x = _maxLeft;
        else if (x > _maxRight) x = _maxRight;

        Vector3 position = new Vector3(x, 0, _zOffset);
        transform.position = position;
    }

}
