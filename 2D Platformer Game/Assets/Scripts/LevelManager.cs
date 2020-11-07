﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script with various level functions
public class LevelManager : MonoBehaviour
{
    //PUBLIC//

    //instance of LevelManager (SINGLETON)
    public static LevelManager instance;

    public float waitToRespawn; //how long to wait until the Player is respawned (in seconds)

    public int gemScoreCollected; //denotes the score of the Player
    public int fireScoreCollected; //denotes the number of fires collected by the Player

    //next level to load
    public string nextLevelToLoad;


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

    public void AddGemScore(int score)
    {
        gemScoreCollected += score;
        //update UI
        UIController.instance.UpdateGemScoreDisplay();
    }

    public void AddFireScore()
    {
        fireScoreCollected++;
        //update UI
        UIController.instance.UpdateFireScoreDisplay();
    }

    public void RemoveFireScore()
    {
        fireScoreCollected--;
        //update UI
        UIController.instance.UpdateFireScoreDisplay();
    }


    public void RespawnPlayer()
    {
        //start the Respawn coroutine
        StartCoroutine(RespawnCoroutine());
    }


    //coroutine - happends outside the normal execution flow of Unity
    //type must be IEnumerator
    private IEnumerator RespawnCoroutine()
    {
        //first, deactivate the player
        //gameObject represents the game object the PlayerController script is attached to (Player)
        PlayerController.instance.gameObject.SetActive(false);

        //play Player death sound effect
        AudioManager.instance.PlaySFX(AudioManager.SoundEffects.PlayerDeath);

        //activate the fading black screen

        //wait for a certain amount of time
        //WaitForSeconds suspends the coroutine execution for the given amount of time
        //take away the fade screen time
        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeScreenSpeed));

        //fade the screen to black
        UIController.instance.FadeToBlack();

        //wait for the screen to fade
        yield return new WaitForSeconds((1f / UIController.instance.fadeScreenSpeed) + .2f);

        //fade the screen to normal
        UIController.instance.FadeFromBlack();

        //reactivate the Player
        PlayerController.instance.gameObject.SetActive(true);

        //respawn the player at correct position (last activated checkpoint)
        PlayerController.instance.SetPlayerPosition(CheckpointController.instance.spawnPoint);

        //reset the Player's health
        PlayerHealthController.instance.ResetHealth();
          
    }


    public void EndLevel()
    {
        //start the level end coroutine - happens over time
        StartCoroutine(EndLevelCoroutine());
    }

    private IEnumerator EndLevelCoroutine()
    {
        //disable the input
        PlayerController.instance.stopInput = true;
        //stop the Camera follow
        CameraController.instance.stopFollowingThePlayer = true;

        //display the level complete text
        UIController.instance.levelCompleteText.SetActive(true);

        //wait 1.5sec
        yield return new WaitForSeconds(1.5f);

        //fade to black
        UIController.instance.FadeToBlack();

        //wait a little more
        yield return new WaitForSeconds((1 / UIController.instance.fadeScreenSpeed) + .25f);

        //store the current scene's name as UNLOCKED in PlayerPrefs
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);

        //load the next level
        SceneManager.LoadScene(nextLevelToLoad);
    }

}
