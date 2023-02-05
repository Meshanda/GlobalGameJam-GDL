using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Variables;
using TMPro;
using UnityEngine;

public class PNJ : GenericSingleton<PNJ>
{
    [SerializeField] private TMP_Text _idleText;
    [SerializeField] private BoolVariable _hasHache;
    
    private bool _isInteractable;
    private int _index;
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement.Instance.GetComponent<Interact>().NearPNJ = true;
        ResetDialogue();
        _idleText.gameObject.SetActive(true);
        _isInteractable = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerMovement.Instance.GetComponent<Interact>().NearPNJ = true;
        _idleText.gameObject.SetActive(false);
        _isInteractable = false;
    }

    private void ResetDialogue()
    {
        _index = 0;

        _idleText.text = _hasHache.value ? "Woaw you found it!!!" : "Hey! Listen!";
    }

    public void NextDialogue()
    {
        if (!_isInteractable) return;

        _index++;
        if (_index > 3)
            _index = 0;

        if (!_hasHache.value)
        {
            _idleText.text = _index switch
            {
                0 => "Hey! Listen!",
                1 => "Some evil roots spread into our sacred place!",
                2 => "Can you find the mighty axe to break those roots, please ?",
                3 => "Pleeeeeeease",
                _ => _idleText.text
            };
        }
        else
        {
            _idleText.text = _index switch
            {
                0 => "Woaw you found it!!!",
                1 => "The power of the mighty axe allows you to destroy those roots.",
                2 => "Find them all and make them go away!",
                3 => "I can't thank you enough for your help!!",
                _ => _idleText.text
            };
        }
        
    }
}
