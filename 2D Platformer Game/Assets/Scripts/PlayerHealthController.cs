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

    //denotes how long the player will be invincible (in seconds)
    public float invincibilityDuration;

    //Player's Sprite Renderer
    public SpriteRenderer playerSpriteRenderer;



    //PRIVATE//

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

            //gameObject represents the game object this script is attached to (Player)
            //remove the player from the world
            gameObject.SetActive(false);
        }
        else
        {
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
}
