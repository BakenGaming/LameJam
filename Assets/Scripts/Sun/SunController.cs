using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour, ISunController
{
    private Vector2 reductionAmount;
    private float meltTimer;
    private bool isInitialized;
    private bool isInSun;
    public void Initialize()
    {
        reductionAmount = GameManager.i.GetLevelStats().meltAmount;
        meltTimer = 0f;
        isInitialized = true;
    }

    #region Loop
    private void Update() 
    {
        if(isInitialized) UpdateTimers();
    }

    private void UpdateTimers()
    {
        if (isInSun) 
        {
            meltTimer -= Time.deltaTime;
        }
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
                TextPopUp.Create(_trigger.transform.position, "Ouch");
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
                TextPopUp.Create(_trigger.transform.position, "Ouchy");
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
            meltTimer = 0f;
        }
    }
    #endregion
}
