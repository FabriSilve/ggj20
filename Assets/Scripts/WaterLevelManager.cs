using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelManager : MonoBehaviour
{

     private float step = 0.001f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseWaterLevel()
    {
        gameObject.transform.Translate(Vector3.up * step);
    }
}
