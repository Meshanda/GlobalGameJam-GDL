using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private List<GameObject> _texts;

    private bool _lookingLeft;
    private void Update()
    {
        RotatePopUps(_player.transform.localScale.x);
    }

    private void RotatePopUps(float xScale)
    {
        foreach (var text in _texts)
        {
            text.transform.localScale = new Vector3(xScale,
                                                        text.transform.localScale.y,
                                                        text.transform.localScale.z);
        }
    }
}
