using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroToBgMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip introMusic;
    public AudioClip bgMusic;
    public float duration = 5f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = introMusic;
        audioSource.Play();
        Invoke("PlayBgMusic", duration);
    }

    // Update is called once per frame
    void PlayBgMusic()
    {
        audioSource.clip = bgMusic;
        audioSource.Play();
    }
}
