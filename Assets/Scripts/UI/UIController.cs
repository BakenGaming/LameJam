using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    public static event Action onInitialMovement;
    [SerializeField] private TextMeshProUGUI _percentText;
    [SerializeField] private GameObject gameStartMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject endMenu;

    private void OnEnable() 
    {
        GameManager.onGameReady += Initialize;
        SizeHandler.onSizeChanged += UpdatePercentText;   
        PlayerInputController_Platformer.OnPauseGame += OpenPauseMenu;
        PlayerInputController_Platformer.OnUnpauseGame += ClosePauseMenu; 
        SizeHandler.onPlayerDied += OpenDeathMenu;
        Refreezer.onExitReached += ExitReached;
    }
    private void OnDisable() 
    {
        GameManager.onGameReady -= Initialize;    
        SizeHandler.onSizeChanged -= UpdatePercentText;
        PlayerInputController_Platformer.OnPauseGame -= OpenPauseMenu;
        PlayerInputController_Platformer.OnUnpauseGame -= ClosePauseMenu;
        SizeHandler.onPlayerDied -= OpenDeathMenu;
        Refreezer.onExitReached -= ExitReached;
    }
    private void Initialize()
    {
        _percentText.text = "100%";
        gameStartMenu.SetActive(true);
    }

    public void CloseGameStartMenu()
    {
        gameStartMenu.SetActive(false);
        GameManager.i.UnPauseGame();
    }

    private void UpdatePercentText(float _percent)
    {
        if(_percent == 0f) _percentText.text = "0%";
        else _percentText.text = _percent.ToString("##") + "%";
    }

    public void InitiateGame()
    {
        onInitialMovement?.Invoke();
    }

    private void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        GameManager.i.PauseGame();
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        GameManager.i.UnPauseGame();
    }

    private void OpenDeathMenu()
    {
        GameManager.i.PauseGame();
        deathMenu.SetActive(true);
    }

    private void CloseDeathMenu()
    {
        deathMenu.SetActive(false);
    }

    public void RestartGame()
    {
        SceneController.StartGame();
    }
    public void BackToMainMenu()
    {
        SceneController.LoadMainMenu();
    }

    private void ExitReached()
    {
        GameManager.i.PauseGame();
        endMenu.SetActive(true);
    }

    public void ExitGame()
    {
        SceneController.ExitGame();
    }

}
