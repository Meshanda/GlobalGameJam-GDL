using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
    [SerializeField] private StringVariable _endScreen;
    
    public void Win()
    {
        Time.timeScale = 0;

        SceneManager.LoadSceneAsync(_endScreen.value, LoadSceneMode.Additive);
    }
}
