using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleBehavior : MonoBehaviour
{
    public float sinkingMultiplier = 1.0f;

    // Update is called once per frame
    void Update()
    {
        ApplicationManager.Instance.AddWater(Time.deltaTime * sinkingMultiplier);
    }
}
