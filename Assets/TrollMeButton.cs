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
        text.text = "We honestly did no thad enough time to add more functionality";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
