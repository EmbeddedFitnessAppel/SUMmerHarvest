using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : Singleton<AudioPlayer>
{

//Attach this to the camera and give that an AudioSource! If you want to double-check settings, check the GameAudio.unity scene
    [Header("Sound clips")]
    public AudioClip AppleCatchSound;
    public AudioClip BasketSuccessSound;
    public AudioClip BasketFailureSound;


    private AudioSource audioSource;

    void Start()
    {
        this.audioSource = this.GetComponent<AudioSource>();
    }

    public void PlaySuccessSound()
    {
        PlaySound(BasketSuccessSound);
    }
    public void PlayCatchSound()
    {
        PlaySound(AppleCatchSound);
    }
    public void PlayFailureSound()
    {
        PlaySound(BasketFailureSound);
    }

    void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
