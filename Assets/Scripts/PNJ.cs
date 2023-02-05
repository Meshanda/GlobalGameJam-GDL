using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ : MonoBehaviour
{
    [SerializeField] private GameObject _npcText;
    private void OnTriggerEnter2D(Collider2D other)
    {
        _npcText.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _npcText.SetActive(false);
    }
}
