using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public  static AudioSource audioSrc;
    public static AudioClip Health;
    public static AudioClip Click;
    public static AudioClip maxHealth;
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        Health = Resources.Load<AudioClip>("pickUpItem");
        Click = Resources.Load<AudioClip>("Menu");
    }

    public static void pickUpHealth()
    {
        audioSrc.PlayOneShot(Health);
    }
    public static void playClick()
    {
        audioSrc.PlayOneShot(Click);
    }

    public void ChangeBGM(AudioClip music)
    {
        audioSrc.Stop();
        audioSrc.clip = music;
        StartCoroutine(playMusic());
    }

    IEnumerator playMusic()
    {
        yield return new WaitForSeconds(1);
        audioSrc.Play();
    }
    public void stopMusic()
    {
        audioSrc?.Stop();
    }
}
