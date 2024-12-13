using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFlakeHandler : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        Destroy(gameObject);
    }
}
