using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    [SerializeField]
    GameObject ship;

    [SerializeField]
    float roundTime; // Time to reach goal (in seconds)

    [SerializeField]
    int startingPosition;

    [SerializeField]
    int endingPosition;

    float elapsedTime = 0;

    void Update()
    {
        if (elapsedTime < roundTime && ship.transform.position.x < endingPosition)
        {
            elapsedTime += Time.deltaTime;
            float newPos = (endingPosition - startingPosition) * elapsedTime / roundTime;
            ship.transform.position = new Vector3(newPos, ship.transform.position.y, ship.transform.position.z);
        }
    }
}
