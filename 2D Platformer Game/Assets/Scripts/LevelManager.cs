using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script with various level functions
public class LevelManager : MonoBehaviour
{
    //PUBLIC//

    //instance of LevelManager (SINGLETON)
    public static LevelManager instance;

    public float waitToRespawn; //how long to wait until the Player is respawned (in seconds)

    public int gemScoreCollected; //denotes the score of the Player
    public int fireScoreCollected; //denotes the number of fires collected by the Player




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

        //wait for a certain amount of time
        //WaitForSeconds suspends the coroutine execution for the given amount of time
        yield return new WaitForSeconds(waitToRespawn);

        //reactivate the Player
        PlayerController.instance.gameObject.SetActive(true);

        //respawn the player at correct position (last activated checkpoint)
        PlayerController.instance.SetPlayerPosition(CheckpointController.instance.spawnPoint);

        //reset the Player's health
        PlayerHealthController.instance.ResetHealth();
          
    }

}
