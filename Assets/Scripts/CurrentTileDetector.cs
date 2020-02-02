using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTileDetector : MonoBehaviour
{
    private GameObject currentTile;
    
    public bool IsOnHole() {
        return currentTile != null && currentTile.tag == "Hole";
    }

    public GameObject GetCurrentTile()
    {
        return currentTile;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Tile" || collider.gameObject.tag == "Hole")
        {
            currentTile = collider.gameObject;
        }
    }
}
