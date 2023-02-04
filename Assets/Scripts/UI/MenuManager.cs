using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Variables;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private StringVariable currentScene;
    public void Quit()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void Play()
    {
        SceneBooter.Instance.LoadGameScenes();
        SceneBooter.Instance.UnloadScene(currentScene);
    }
}
