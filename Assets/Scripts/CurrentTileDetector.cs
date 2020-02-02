using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTileDetector : MonoBehaviour
{
    private GameObject currentTile;

    public GameObject GetCurrentTile()
    {
        return currentTile;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Tile")
        {
            currentTile = collider.gameObject;
        }
    }
}
