using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeHandler : MonoBehaviour, ISizeHandler
{
    public static event Action<float> onSizeChanged;
    public static event Action onPlayerDied;
    private Vector2 playerScale;

    public void Initialize()
    {
        playerScale = transform.localScale;
        SnowFlakeHandler.onSnowFlakeCollected += IncreaseSize;
    }

    private void OnDisable() 
    {
        SnowFlakeHandler.onSnowFlakeCollected -= IncreaseSize;    
    }
    public void IncreaseSize(Vector2 _sizeAdjustmentValueGrow)
    {
        playerScale += _sizeAdjustmentValueGrow;
        transform.localScale = playerScale;
        float _percent = (playerScale.x / 1f) * 100f;
        onSizeChanged?.Invoke(_percent);
    }

    public void ReduceSize(Vector2 _sizeAdjustmentValueMelt)
    {
        playerScale -= _sizeAdjustmentValueMelt;
        if(playerScale.x <= 0f)
        {
            onSizeChanged?.Invoke(0); 
            GameManager.i.PauseGame(); 
            SoundManager.PlaySound(SoundManager.Sound.playerDie);
            onPlayerDied?.Invoke();
            Destroy(gameObject);
        }
        else
        {
            transform.localScale = playerScale;  
            float _percent = (playerScale.x / 1f) * 100f;
            onSizeChanged?.Invoke(_percent);  
        }
    }
}
