using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {
    public AudioClip[] PunchSounds;
    public AudioClip[] SwingSounds;
    public AudioClip[] PainSounds;
    private AudioSource Source;

    void Start()
    {
        Source = gameObject.GetComponent<AudioSource>();
    }

    public void PlayPunchSound()
    {
        //play random punch sound
        Source.PlayOneShot(PunchSounds[Random.Range(0, PunchSounds.Length)]);
    }
    public void PlaySwingSound()
    {
        //play random Swing sound
        Source.PlayOneShot(SwingSounds[Random.Range(0, SwingSounds.Length)]);
    }
    public void PlayPainSound()
    {
        //play random Pain sound
        Source.PlayOneShot(PainSounds[Random.Range(0, PainSounds.Length)]);
    }
}
