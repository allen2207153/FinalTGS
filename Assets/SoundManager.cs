using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSrc;
    public AudioClip attack;
    public AudioClip jump;
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
   public  void audio_attack()
    {
        audioSrc.PlayOneShot(attack);
    }
   public  void audio_jump()
    {
        audioSrc.PlayOneShot(jump);
    }
}
