using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint Current;
    private static GameObject Player;

    public static void ReturnPlayer()
    {
        if (Current != null && Player != null)
        {
            Player.transform.position = Current.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (Current != this)
            {
                Player = col.gameObject;
                Current = this;
                print("Checkpoint set");
            }
        }
    }
}
