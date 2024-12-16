using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFlakeHandler : MonoBehaviour, ICollectable
{
    public static event Action<Vector2> onSnowFlakeCollected;
    [SerializeField] private Vector2 _increaseAmount;
    public void Collect()
    {
        onSnowFlakeCollected?.Invoke(_increaseAmount);
        SoundManager.PlaySound(SoundManager.Sound.collect);
        Destroy(gameObject);
    }
}
