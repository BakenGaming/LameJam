using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _percentText;
    [SerializeField] private GameObject pauseMenu;

    private void OnEnable() 
    {
        GameManager.onGameReady += Initialize;
        SizeHandler.onSizeChanged += UpdatePercentText;   
        PlayerInputController_Platformer.OnPauseGame += OpenPauseMenu;
        PlayerInputController_Platformer.OnUnpauseGame += ClosePauseMenu; 
    }
    private void OnDisable() 
    {
        GameManager.onGameReady -= Initialize;    
        SizeHandler.onSizeChanged -= UpdatePercentText;
        PlayerInputController_Platformer.OnPauseGame -= OpenPauseMenu;
        PlayerInputController_Platformer.OnUnpauseGame -= ClosePauseMenu;
    }
    private void Initialize()
    {
        _percentText.text = "100%";
    }

    private void UpdatePercentText(float _percent)
    {
        if(_percent == 0f) _percentText.text = "0%";
        else _percentText.text = _percent.ToString("##") + "%";
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

}
