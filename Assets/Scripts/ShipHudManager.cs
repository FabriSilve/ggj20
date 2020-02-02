using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHudManager : MonoBehaviour
{
    public UnityEngine.UI.Image image;
    [SerializeField]
    private float shipWidth;

    private float elapsedTime;

    void Awake() {
        image = GetComponent<UnityEngine.UI.Image>();
        shipWidth = image.rectTransform.rect.width;
    } 

    void Start()
    {
        
    }

    void Update()
    {
        image.enabled = ApplicationManager.Instance.currentGameStatus == ApplicationManager.GameStatus.Playing;

        float newX = shipWidth / 2 + (Screen.width - shipWidth) * ApplicationManager.Instance.GetProgress();
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
