using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour, ISunController
{
    #region Variables
    private const float _constantMeltSpeed = .25f;
    private Vector2 _constantReductionSize = new Vector2(.01f, .01f);
    private Vector2 reductionAmount;
    private float meltTimer, constantMeltTimer, exitTimer;
    private bool isInitialized, isInSun, sunActive;
    private ISizeHandler _playerHandler;
    private Rigidbody2D sunRB;
    private float sunSpeed;
    #endregion

    #region Initialize
    public void Initialize()
    {
        reductionAmount = GameManager.i.GetLevelStats().meltAmount;
        _playerHandler = GameManager.i.GetPlayerGO().GetComponent<ISizeHandler>();
        meltTimer = 0f;
        sunSpeed = GameManager.i.GetLevelStats().sunSpeed;
        constantMeltTimer = _constantMeltSpeed;
        sunRB = GetComponent<Rigidbody2D>();
        isInitialized = true;
        UIController.onInitialMovement += ActivateSun;
        
    }
    private void ActivateSun()
    {
        sunActive = true;
    }

    private void OnDisable() 
    {
        UIController.onInitialMovement -= ActivateSun;    
    }

    #endregion

    #region Loop
    private void Update() 
    {
        if(isInitialized) UpdateTimers();
        if(GameManager.i.GetIsPaused() && sunActive) sunActive = false;
        if(!GameManager.i.GetIsPaused() && !sunActive) sunActive = true;
    }

    private void UpdateTimers()
    {
#if UNITY_EDITOR
    if (Input.GetKeyDown(KeyCode.K)) sunActive = false;

    if (Input.GetKeyDown(KeyCode.L)) sunActive = true;

#endif
        exitTimer -= Time.deltaTime;
        if(exitTimer <= 0) meltTimer -= Time.deltaTime;
        constantMeltTimer -= Time.deltaTime;
        if(constantMeltTimer < 0f && sunActive) 
        {
            _playerHandler.ReduceSize(_constantReductionSize);
            constantMeltTimer = _constantMeltSpeed;
        }
    }

    private void FixedUpdate() 
    {
        if(!sunActive) 
        {
            sunRB.velocity = Vector2.zero;
            return;
        }
        sunRB.velocity = new Vector2 (sunSpeed, sunRB.velocity.y);    
    }
    #endregion
    #region Triggers
    private void OnTriggerEnter2D(Collider2D _trigger) 
    {
        if(!sunActive) return;
        ISizeHandler _handler = _trigger.GetComponent<ISizeHandler>();
        if( _handler != null )
        {
            if(meltTimer <= 0 || exitTimer <= 0) 
            {
                Debug.Log("Entered Sun");
                TextPopUp.Create(_trigger.transform.Find("Sprite").Find("TextSpawn").position, "Ouch");
                isInSun = true;
                _handler.ReduceSize(reductionAmount);
                meltTimer = GameManager.i.GetLevelStats().meltIntervals;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D _trigger) 
    {
        if(!sunActive) return;
        ISizeHandler _handler = _trigger.gameObject.GetComponent<ISizeHandler>();
        if(_handler != null)
        {
            if(meltTimer <= 0)
            {
                Debug.Log("In Sun");
                TextPopUp.Create(_trigger.transform.Find("Sprite").Find("TextSpawn").position, "Ouchy");
                _handler.ReduceSize(reductionAmount);
                meltTimer = GameManager.i.GetLevelStats().meltIntervals;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D _trigger) 
    {
        if(!sunActive) return;
        ISizeHandler _handler = _trigger.GetComponent<ISizeHandler>();
        if( _handler != null ) 
        {
            Debug.Log("Exit Sun");
            isInSun = false;
            meltTimer = GameManager.i.GetLevelStats().meltIntervals;
            exitTimer = .3f;
        }
    }
    #endregion
}
