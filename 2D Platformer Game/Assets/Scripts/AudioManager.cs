using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // PUBLIC // 

    public static AudioManager instance;

    //array of sound effects
    public AudioSource[] soundEffects;

    public enum SoundEffects
    {
        BossHit = 0,
        BossImpact,
        Shot, //Boss and Player shot
        EnemyExplode,
        LevelSelected,
        MapMovement,
        PickupGem,
        PickupHealth,
        PlayerDeath,
        PlayerHurt,
        PlayerJump,
        WarpJingle
    }

    //audio sources
    public AudioSource backgroundMusic;
    public AudioSource levelEndMusic;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(SoundEffects soundToPlay)
    {
        //if the selected sound is already playing - stop it
        //if it's not playing - Stop() won't do anything
        soundEffects[(int)soundToPlay].Stop();

        //add little variance to the sound (so it doesn't getboring to hear)
        //change the pitch 
        soundEffects[(int)soundToPlay].pitch = Random.Range(.9f, 1.1f);

        soundEffects[(int)soundToPlay].Play();
    }
}
