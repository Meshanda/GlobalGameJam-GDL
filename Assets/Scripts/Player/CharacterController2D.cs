using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    private bool _grounded;

    private BoxCollider2D _boxCollider;
    public BoxCollider2D Collider2D => _boxCollider;

    private float _angleError = 0.005f;
    
    public bool IsGrounded => _grounded;

    private Vector2 _velocity;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void LateUpdate()
    {
        _grounded = false;

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position.ConvertTo<Vector2>() +  ( _boxCollider.offset/2), _boxCollider.bounds.size, 0);
        foreach (Collider2D hit in hits)
        {
            if (hit == _boxCollider || hit.isTrigger)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(_boxCollider);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
            }
            
            Debug.DrawRay(colliderDistance.pointA, colliderDistance.normal);

            if (Vector2.Angle(colliderDistance.normal, Vector2.up) < (90 - _angleError) && _velocity.y <= 0)
            {
                _grounded = true;
            }
        }
    }

    public void Move(Vector2 motion)
    {
        _velocity = motion;
        transform.Translate(motion);
    }
}
