using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour, ISunController
{
    #region Variables
    private const float _constantMeltSpeed = 2f;
    private Vector2 _constantReductionSize = new Vector2(.01f, .01f);
    private Vector2 reductionAmount;
    private float meltTimer, constantMeltTimer;
    private bool isInitialized;
    private bool isInSun;
    private ISizeHandler _playerHandler;
    private Rigidbody2D sunRB;
    private float sunSpeed = 50f;
    #endregion

    #region Initialize
    public void Initialize()
    {
        reductionAmount = GameManager.i.GetLevelStats().meltAmount;
        _playerHandler = GameManager.i.GetPlayerGO().GetComponent<ISizeHandler>();
        meltTimer = 0f;
        constantMeltTimer = _constantMeltSpeed;
        sunRB = GetComponent<Rigidbody2D>();
        isInitialized = true;
    }

    #endregion

    #region Loop
    private void Update() 
    {
        if(isInitialized) UpdateTimers();
    }

    private void UpdateTimers()
    {

        meltTimer -= Time.deltaTime;
        constantMeltTimer -= Time.deltaTime;
        if(constantMeltTimer < 0f) 
        {
            _playerHandler.ReduceSize(_constantReductionSize);
            constantMeltTimer = _constantMeltSpeed;
        }
    }

    private void FixedUpdate() 
    {
        sunRB.velocity = new Vector2 (sunSpeed * Time.deltaTime, sunRB.velocity.y);    
    }
    #endregion
    #region Triggers
    private void OnTriggerEnter2D(Collider2D _trigger) 
    {
        
        ISizeHandler _handler = _trigger.GetComponent<ISizeHandler>();
        if( _handler != null )
        {
            
            if(meltTimer <= 0) 
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
        
        ISizeHandler _handler = _trigger.GetComponent<ISizeHandler>();
        if( _handler != null ) 
        {
            Debug.Log("Exit Sun");
            isInSun = false;
            meltTimer = .03f;
        }
    }
    #endregion
}
