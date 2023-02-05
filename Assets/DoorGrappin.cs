using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGrappin : MonoBehaviour
{
    public GameObject teleportTo;
    private GameObject _player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && _player)
        {
            if (teleportTo)
            {
                GetComponent<TransitionCamera>()?.Transition();
                _player.transform.position = teleportTo.transform.position;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _player = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _player = null;
        }
    }
}
