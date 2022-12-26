using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource[] sounds;

    public static AudioManager instanse;

    public void Awake()
    {
        instanse = this;
    }

    public void PlaySound(int number)
    {
        sounds[number].Play();
    }
}
