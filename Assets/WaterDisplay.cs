using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDisplay : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI waterText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ApplicationManager.Instance.currentGameStatus == ApplicationManager.GameStatus.Playing)
        {
            float percent = ApplicationManager.Instance.currentAmountOfWater / ApplicationManager.Instance.maxAmountOfWaterAllowed;

            if (percent >= 0)
            {

                percent = 100 - (percent * 100);

                waterText.text = percent.ToString("F2") + "%";
            }
        }




    }
}
