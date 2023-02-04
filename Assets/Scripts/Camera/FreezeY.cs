using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeY : MonoBehaviour
{
    [SerializeField] private Transform _camTfm;
    [SerializeField] private float Y;

    private void Update()
    {
        _camTfm.position = new Vector2(_camTfm.position.x, Y);
    }
}
