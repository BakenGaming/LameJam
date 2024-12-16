using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Awake() 
    {
        SoundManager.PlayMusic(SoundManager.Music.defaultMusic);    
    }
    public void PlayGame()
    {
        SceneController.StartGame();
    }

    public void ExitGame()
    {
        SceneController.ExitGame();
    }
}
