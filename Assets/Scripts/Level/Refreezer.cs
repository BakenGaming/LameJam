using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refreezer : MonoBehaviour
{
    public static event Action onExitReached;
    private void OnTriggerEnter2D(Collider2D _trigger) 
    {
        Debug.Log("Triggered");
        onExitReached?.Invoke();
    }
}
