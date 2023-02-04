using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Variables;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBooter : GenericSingleton<SceneBooter>
{
    [SerializeField] private List<StringVariable> _sceneToLoadOnBoot;
    [SerializeField] private List<StringVariable> _gameScenes;

    private void Start()
    {
        foreach (var scene in _sceneToLoadOnBoot)
        {
            SceneManager.LoadSceneAsync(scene.value, LoadSceneMode.Additive);
        }
    }

    public void LoadGameScenes()
    {
        foreach (var scene in _gameScenes)
        {
            SceneManager.LoadSceneAsync(scene.value, LoadSceneMode.Additive);
        }
    }

    public void UnloadScene(StringVariable sceneToUnload)
    {
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync(sceneToUnload.value);
        }
    }
}