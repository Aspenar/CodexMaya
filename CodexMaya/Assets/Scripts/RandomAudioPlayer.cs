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
        Invoke("RandomSounds", 20);
    }

    void RandomSounds()
    {
        randomAudio.clip = audioClips[Random.Range(0, audioClips.Length)];
        randomAudio.Play();
        callAudio();
    }
}
