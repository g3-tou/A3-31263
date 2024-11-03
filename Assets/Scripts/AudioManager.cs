using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroToBgMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip introMusic;
    //public AudioClip bgMusic;
    private float duration = 3f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = introMusic;
        audioSource.Play();
        //Invoke("StopMusic", duration);
    }

    // Update is called once per frame
    /*void PlayBgMusic()
    {
        audioSource.clip = bgMusic;
        audioSource.Play();
    }*/

    void StopMusic(){
        audioSource.Stop();
    }
}
