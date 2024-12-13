using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISizeHandler
{
    public void Initialize();
    public void ReduceSize(Vector2 _value);
    public void IncreaseSize(Vector2 _value);
}
