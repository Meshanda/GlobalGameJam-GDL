using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Variables;
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
        if (!_shakePlatform)
        {
            _startPosition = transform.position;
            StartCoroutine(DestroyPlatformCoroutine());
            _shakePlatform = true;
        }
    }

    private IEnumerator DestroyPlatformCoroutine()
    {
        yield return new WaitForSeconds(_timeUntilBreak);

        // Destroy this gameobject
        _shakePlatform = false;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(_timeUntilBreak);
        
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
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
    