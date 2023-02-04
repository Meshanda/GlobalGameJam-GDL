using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Variables;
using UnityEngine;

public class Rune : MonoBehaviour
{
    [Serializable]
    public enum RuneType
    {
        DoubleJump,
        Dash,
        Grappin,
        WallJump,
        Hache
    }

    [SerializeField] private RuneType _runeType;

    [SerializeField] private BoolVariable b_doubleJump;
    [SerializeField] private BoolVariable b_dash;
    [SerializeField] private BoolVariable b_grappin;
    [SerializeField] private BoolVariable b_wallJump;
    [SerializeField] private BoolVariable b_hache;

    public void UnlockPower()
    {
        switch (_runeType)
        {
            case RuneType.DoubleJump:
                b_doubleJump.value = true;
                break;
            case RuneType.Dash:
                b_dash.value = true;
                break;
            case RuneType.Grappin:
                b_grappin.value = true;
                break;
            case RuneType.WallJump:
                b_wallJump.value = true;
                break;
            case RuneType.Hache:
                b_hache.value = true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

