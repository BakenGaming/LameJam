using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _mainCam;
    private Transform _cameraTarget;
    private bool isMoving;
    private float _cameraZoom, _defaultOrthoSize;
    private void OnEnable() 
    {
        GameManager.onGameReady += Initialize;
        SizeHandler.onSizeChanged += CameraZoom;    
    }
    private void OnDisable() 
    {
        GameManager.onGameReady -= Initialize;  
        SizeHandler.onSizeChanged -= CameraZoom;  
    }
    private void Initialize()
    {
        Debug.Log("Initialize Camera");
        _mainCam = Camera.main;
        _defaultOrthoSize = _mainCam.orthographicSize;
        _cameraTarget = GameManager.i.GetPlayerGO().transform;
        isMoving = true;
    }

    private void LateUpdate() 
    {
        if (isMoving && _cameraTarget != null)
            _mainCam.transform.position = new Vector3(_cameraTarget.position.x, _cameraTarget.position.y, _mainCam.transform.position.z);    
    }

    private void CameraZoom(float _amount)
    {
        _cameraZoom = Mathf.Clamp(_defaultOrthoSize * (_amount/100f), 2f, 10f);
        _mainCam.orthographicSize = _cameraZoom;
        
    }


    
}
