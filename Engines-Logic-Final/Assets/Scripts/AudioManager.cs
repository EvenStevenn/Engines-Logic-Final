using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource music;
    public AudioSource enemySpawnAudio;
    public AudioSource projectileAudio;
    public AudioSource enemyDeathAudio;


    void Awake()
    {
        instance = this;
        PlayMusic();
    }

    public void PlayEnemySpawnSound()
    {
        enemySpawnAudio.Play();
    }

    public void PlayEnemyDeathSound()
    {
        enemyDeathAudio.Play();
    }

    public void PlayMusic()
    {
        music.Play();
    }

    public void PlayProjectileSound()
    {
        projectileAudio.Play();
    }


   
}



