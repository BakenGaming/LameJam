using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    [SerializeField] private Vector3 _targetScaleDown;
    [SerializeField] private Vector3 _targetScaleUp;
    [SerializeField] private float _scaleSpeed;
    private Vector3 _currentScale;
    private bool _scalingDown;

    private void Start() 
    {
        _scalingDown = true; 
        _currentScale = transform.localScale;  
    }

    private void Update() 
    {
        if(_scalingDown)
        {
            _currentScale = Vector3.MoveTowards(_currentScale, _targetScaleDown, _scaleSpeed * Time.deltaTime);
            transform.localScale = _currentScale;
            if(_currentScale == _targetScaleDown) _scalingDown = false;
        }
        else
        {
            _currentScale = Vector3.MoveTowards(_currentScale, Vector3.one, _scaleSpeed  * Time.deltaTime);
            transform.localScale = _currentScale;
            if(_currentScale == Vector3.one) _scalingDown = true;
        }
    }
}
