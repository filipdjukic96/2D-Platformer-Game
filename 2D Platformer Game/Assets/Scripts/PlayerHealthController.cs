using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    //PUBLIC//

    //instance of PlayerHealthController (SINGLETON)
    //make public so it's accessible from other scripts
    public static PlayerHealthController instance;
    
    public int currentHealth;
    public int maxHealth;

    //reference to Player death effect
    public GameObject deathEffect;

    //denotes how long the player will be invincible (in seconds)
    public float invincibilityDuration;




    //PRIVATE//

    //Player's Sprite Renderer
    private SpriteRenderer playerSpriteRenderer;

    //used to count down how much invicibility time has remained 
    private float invincibilityCounter;

    //called before Start()
    private void Awake()
    {
        instance = this; //set singleton instance
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        playerSpriteRenderer = GetComponent<SpriteRenderer>(); //gets the Sprite Renderer component attached to the object this script is attached to
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime; //Time.deltaTime is time between two calls of Update (1/60 for 60 FPS)

            if(invincibilityCounter <= 0)
            {
                //DISABLE INVINCIBILITY
                //player is no longer invincible - no longer see-through
                playerSpriteRenderer.color = new Color(playerSpriteRenderer.color.r,
                                                       playerSpriteRenderer.color.g,
                                                       playerSpriteRenderer.color.b,
                                                       1f);
            }
        }
        
    }

    public void DamagePlayer()
    {
        //if invincible, return
        if(invincibilityCounter > 0)
        {
            return;
        }


        currentHealth--; //take 1 away from current player health

        //if player died
        if(currentHealth <= 0)
        {
            currentHealth = 0; //can't go below 0

            //show Player death effect (ONLY FROM HERE - IF THE PLAYER IS KILLED WHEN SOMETHING DAMAGES HIM!)
            //effect must be called before respawning the player
            Instantiate(deathEffect, transform.position, transform.rotation);

            //respawn the Player
            LevelManager.instance.RespawnPlayer();
        }
        else
        {
            //health is > 0
            //play Player hurt sound effect
            AudioManager.instance.PlaySFX(AudioManager.SoundEffects.PlayerHurt);

            //SET INVINCIBILITY
            invincibilityCounter = invincibilityDuration;
            //set player see-through
            playerSpriteRenderer.color = new Color(playerSpriteRenderer.color.r,
                                                   playerSpriteRenderer.color.g,
                                                   playerSpriteRenderer.color.b,
                                                   playerSpriteRenderer.color.a * 0.5f);

            //set knockback
            PlayerController.instance.KnockBack();
        }

        UIController.instance.UpdateHealthDisplay(); //update hearts UI
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UIController.instance.UpdateHealthDisplay();//update the hearts in UI
    }

    public void KillPlayer()
    {
        currentHealth = 0;
        UIController.instance.UpdateHealthDisplay();
    }

    public void HealPlayer()
    {
        //check just to make sure
        if(currentHealth == maxHealth)
        {
            return;
        }

        currentHealth++;
        UIController.instance.UpdateHealthDisplay(); //update hearts UI
    }
}
