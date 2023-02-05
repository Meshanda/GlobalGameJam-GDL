using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpScript : GenericSingleton<PopUpScript>
{
    [SerializeField] private GameObject _text;
    [SerializeField] private float delay = 1.5f;
    private bool _lookingLeft;

    public void OnTogglePopUp(RoomType _roomType)
    {
        var str = _roomType switch
        {
            RoomType.DoubleJump => "You can now double jump !",
            RoomType.Dash => "You can now dash forward!\nPress J",
            RoomType.Grappin => "You can now use a grappling hook!\n Press K",
            RoomType.WallJump => "You can now wall jump!",
            RoomType.Hache => "You can now cut roots!\n Press L",
            _ => ""
        };
        
        _text.GetComponent<TMP_Text>().text = str;
        _text.SetActive(true);
        StartCoroutine(DestroyPopUp());
    }

    private IEnumerator DestroyPopUp()
    {
        yield return new WaitForSeconds(delay);

        _text.SetActive(false);
    }
}
