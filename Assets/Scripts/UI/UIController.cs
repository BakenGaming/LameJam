using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _percentText;

    private void OnEnable() 
    {
        GameManager.onGameReady += Initialize;
        SizeHandler.onSizeChanged += UpdatePercentText;    
    }
    private void OnDisable() 
    {
        GameManager.onGameReady -= Initialize;    
        SizeHandler.onSizeChanged -= UpdatePercentText;
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


}
