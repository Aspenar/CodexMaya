using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{

    public AudioSource randomAudio;
    public AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        callAudio();
    }

    void callAudio()
    {
        Invoke("randomAudio", 5);
    }
}
