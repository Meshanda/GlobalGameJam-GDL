using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [Tooltip("In second")]
    [SerializeField] private float _timeUntilBreak = 2f;
    [SerializeField] private float _moveFactor = 1f;
    [SerializeField] private float _maxHorizontalMovement = 0.1f;

    private bool _shakePlatform = false;
    private bool _right = true;

    private float _xOffset = 0f;
    private Vector3 _startPosition;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _startPosition = transform.position;
        StartCoroutine(DestroyPlatformCoroutine());
        _shakePlatform = true;
    }

    private IEnumerator DestroyPlatformCoroutine()
    {
        yield return new WaitForSeconds(_timeUntilBreak);

        // Destroy this gameobject
        _shakePlatform = false;

        Destroy(gameObject);
    }

    private void Update()
    {
        if (!_shakePlatform) return;

        _xOffset += (_right ? 1 : -1) * Time.deltaTime * _moveFactor;     

        if (_xOffset > _maxHorizontalMovement)
        {
            _right = false;
            _xOffset = _maxHorizontalMovement;
        }
        else if (_xOffset < -_maxHorizontalMovement)
        {
            _right = true;
            _xOffset = -_maxHorizontalMovement;
        }
        else
        {
            Vector3 offset = new Vector3(_xOffset, 0, 0);
            transform.position = _startPosition + offset;
        }       
    }
}
    