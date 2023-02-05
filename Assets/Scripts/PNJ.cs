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
        
        AudioManager.Instance.Play("pnj");
    }

    public void NextDialogue()
    {
        if (!_isInteractable) return;

        _index++;

        if (_index == 4 && _hasHache.value)
        {
            GameManager.Instance.Win();
            _idleText.gameObject.SetActive(false);
        }
            
        if (_index > 3)
            _index = 0;

        if (!_hasHache.value)
        {
            _idleText.text = _index switch
            {
                0 => "<wave a=0.015 f=0.7>Hey! Listen!</wave>",
                1 => "Some <shake a=0.0035><color=#9F614A>evil roots</color></shake> spread into our <wave a=0.0045><color=#FFD700>sacred place!</color>",
                2 => "Can you find the <incr a=.5 f=2><color=red>mighty axe</color></incr> to break those roots, please ?",
                3 => "<wave a=0.015 f=0.7>Pleeeeeeease</wave>",
                _ => _idleText.text
            };
        }
        else
        {
            _idleText.text = _index switch
            {
                0 => "<wave a=0.015 f=0.75>Woaw you found it!!!",
                1 => "The power of the <incr a=.5 f=2><color=red>mighty axe</color></incr> allows you to destroy those <shake a=0.0035><color=#9F614A>evil roots</color></shake>.",
                2 => "Find them all and make them go away!",
                3 => "<bounce a=0.015 f=.7>I can't thank you enough for your help!!",
                _ => _idleText.text
            };
        }
        
        AudioManager.Instance.Play("pnj");
    }
}
