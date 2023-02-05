using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public bool NearPNJ;
    public bool NearDoor;
    public void OnInteract()
    {
        if (NearPNJ)
        {
            PNJ.Instance.NextDialogue();
        }

        if (NearDoor)
        {
            DoorGrappin.ClickDoor();
        }
    }
}
