using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refreezer : MonoBehaviour
{
    public static event Action onExitReached;
    private bool _triggered=false;
    private void OnTriggerEnter2D(Collider2D _trigger) 
    {
        if(!_triggered)
        {
            _triggered = true;
            SoundManager.PlaySound(SoundManager.Sound.win);
            onExitReached?.Invoke();
        }
        
    }
}
