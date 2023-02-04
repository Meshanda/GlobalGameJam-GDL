using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
