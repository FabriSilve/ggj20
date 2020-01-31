using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    public GameObject ship;
    public float roundTime; // Time to reach goal (in seconds)
    public int padding; // Padding between ship image and the border of the screen

    private float elapsedTime;

    void Start()
    {
        elapsedTime = 0;
        ship.SetActive(true);
    }

    void Update()
    {
        if (elapsedTime < roundTime && ship.transform.position.x < Screen.width - padding)
        {
            elapsedTime += Time.deltaTime;
            float newPos = Screen.width * elapsedTime / roundTime + padding;
            ship.transform.position = new Vector3(newPos, ship.transform.position.y, ship.transform.position.z);
        }
    }
}
