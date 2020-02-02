using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHudManager : MonoBehaviour
{
    public UnityEngine.UI.Image image;
    public int padding; // Padding between ship image and the border of the screen

    private float elapsedTime;

    void Awake() {
        image = GetComponent<UnityEngine.UI.Image>();
    } 

    void Start()
    {
        
    }

    void Update()
    {
        image.enabled = ApplicationManager.Instance.currentGameStatus == ApplicationManager.GameStatus.Playing;

        float newX = Screen.width * ApplicationManager.Instance.GetProgress();
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
