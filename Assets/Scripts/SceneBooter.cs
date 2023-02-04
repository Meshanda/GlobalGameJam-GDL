using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Variables;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBooter : MonoBehaviour, ISingleton
{
    [SerializeField] private List<StringVariable> _sceneToLoad;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        foreach (var scene in _sceneToLoad)
        {
            SceneManager.LoadSceneAsync(scene.value, LoadSceneMode.Additive);
        }
    }
    
}