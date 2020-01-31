using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    AudioSource menuMusic;

    [SerializeField]
    AudioSource playMusic1;
    [SerializeField]
    AudioSource playMusic2;


    //TODO Add audio Effects
    List<AudioSource> allAudioSources = new List<AudioSource>();

    // Start is called before the first frame update
    void Start()
    {
        allAudioSources.Add(menuMusic);
        allAudioSources.Add(playMusic1);
        allAudioSources.Add(playMusic2);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
