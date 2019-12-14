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

    // Starts music on awake
    void Awake()
    {
        instance = this;
        PlayMusic();
    }

    // Audio for when an enemy spawns
    public void PlayEnemySpawnSound()
    {
        enemySpawnAudio.Play();
    }
    
    // Death sound 
    public void PlayEnemyDeathSound()
    {
        enemyDeathAudio.Play();
    }

    // Music sound
    public void PlayMusic()
    {
        music.Play();
    }

    // Projectile sound
    public void PlayProjectileSound()
    {
        projectileAudio.Play();
    }


   
}



