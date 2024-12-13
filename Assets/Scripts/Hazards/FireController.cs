using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] private Vector2 reductionAmount;
    private bool canBurnPlayer = true;
    private void OnTriggerEnter2D(Collider2D _trigger) 
    {
        
        ISizeHandler _handler = _trigger.GetComponent<ISizeHandler>();
        if( _handler != null && canBurnPlayer)
        {
                Debug.Log("Entered Fire");
                TextPopUp.Create(_trigger.transform.Find("Sprite").Find("TextSpawn").position, "Buuuuuuuuuuurn");
                _handler.ReduceSize(reductionAmount);
                canBurnPlayer = false;
                Destroy(gameObject);
        }
    }
}

