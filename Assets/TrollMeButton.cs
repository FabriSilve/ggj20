using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollMeButton : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void TrollText()
    {
        int random = Random.Range(0, 4);
        switch (random)

        {

            case 0:
                text.text = "We honestly did no thad enough time to add more functionality";

                break;
            case 1:
                text.text = "Corona virus is not actually related to the beer.";

                break;
            case 2:
                text.text = "I used to be a back end developer. I switched to pirating for the perks.";

                break;
            case 3:
                text.text = "All you need is love, all you need is love, love is all you need.";

                break;

            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
