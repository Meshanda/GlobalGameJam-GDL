using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
    [SerializeField] private StringVariable _endScreen;
    [SerializeField] private float _endGameDelay = 0f;
    
    public void Win()
    {
        PlayerMovement.Instance.Movable = false;
        AudioManager.Instance.StopPlaying("theme");
        AudioManager.Instance.Play("theme2");
        StartCoroutine(WinRoutine());
    }

    private IEnumerator WinRoutine()
    {
        yield return new WaitForSeconds(_endGameDelay);
        
        
        Time.timeScale = 0;
        SceneManager.LoadSceneAsync(_endScreen.value, LoadSceneMode.Additive);
    }
}
