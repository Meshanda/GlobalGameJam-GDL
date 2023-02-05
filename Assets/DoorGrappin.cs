using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGrappin : MonoBehaviour
{
    public static Action ClickDoor;
    
    public GameObject teleportTo;
    public bool _nearDoor;

    private void OnEnable()
    {
        ClickDoor += OnClickDoor;
    }
    
    private void OnDisable()
    {
        ClickDoor -= OnClickDoor;
    }

    private void OnClickDoor()
    {
        if (PlayerMovement.Instance.gameObject.GetComponent<Interact>().NearDoor && _nearDoor)
        {
            GetComponent<TransitionCamera>()?.Transition();
            Invoke(nameof(MovePlayer), 0.1f);
        }
    }

    void MovePlayer()
    {
        PlayerMovement.Instance.gameObject.transform.position = teleportTo.transform.position;
        Debug.Log($"{gameObject.name}: {teleportTo.transform.position}");
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerMovement.Instance.gameObject.GetComponent<Interact>().NearDoor = true;
            _nearDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerMovement.Instance.gameObject.GetComponent<Interact>().NearDoor = false;
            _nearDoor = false;
        }    
    }
}
