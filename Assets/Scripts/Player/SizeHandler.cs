using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeHandler : MonoBehaviour, ISizeHandler
{
    private Vector2 playerScale;

    public void Initialize()
    {
        playerScale = transform.localScale;
    }
    public void IncreaseSize(Vector2 _sizeAdjustmentValueGrow)
    {
        playerScale += _sizeAdjustmentValueGrow;
        transform.localScale = playerScale;
    }

    public void ReduceSize(Vector2 _sizeAdjustmentValueMelt)
    {
        Debug.Log($"Reduce Size ({playerScale} by {_sizeAdjustmentValueMelt}");
        playerScale -= _sizeAdjustmentValueMelt;
        transform.localScale = playerScale;    
    }
}
