using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpRoom : MonoBehaviour
{
    [SerializeField] private GameObject _rootToSpawn;
    private void OnTriggerEnter2D(Collider2D other)
    {
        SpawnRoot();
    }

    private void SpawnRoot()
    {
        _rootToSpawn.SetActive(true);
    }
}
