using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //PUBLIC//

    //denotes if this is a gem object
    public bool isGemPickup;
    //denotes if this is a fire object
    public bool isFirePickup;
    //denotes if this is a health pickup object
    public bool isHealthPickup;

    //denotes how much the gem is worth (score)
    public int gemScoreWorth;


    //reference to pickup effect game object
    public GameObject pickupEffect;

    //PRIVATE//
    //denotes if the collectible is collected
    private bool isCollected;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !isCollected)
        {
           

            //is it a gem pickup
            if(isGemPickup)
            {
                LevelManager.instance.AddGemScore(gemScoreWorth);
            }
            else if (isFirePickup)
            {
                LevelManager.instance.AddFireScore();
                
            }
            else if (isHealthPickup)
            {
                //only allow health pickups if the player's health is not full
                if(PlayerHealthController.instance.currentHealth == PlayerHealthController.instance.maxHealth)
                {
                    return;
                }

                //add 1/2 hearth
                PlayerHealthController.instance.HealPlayer();
            }
            else
            {
                //do nothing
                return;
            }
            
            isCollected = true;

            //remove object from the scene
            Destroy(gameObject);

            //add pickup effect
            //Instantiate creates a copy (gameObject passed as first arg, position and rotation as 2nd and 3rd)
            Instantiate(pickupEffect, transform.position, transform.rotation);
        }
    }
}
