using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneController.StartGame();
    }

    public void ExitGame()
    {
        SceneController.ExitGame();
    }
}
