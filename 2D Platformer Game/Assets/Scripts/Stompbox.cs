using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stompbox is a Box Collider used for detecting when the Player stomps on an enemy from above

public class Stompbox : MonoBehaviour
{

    //effect of enemy dying
    public GameObject deathEffect;

    //collectible which will be dropped (cherry or fire)
    public GameObject collectibleCherry;
    public GameObject collectibleFire;
    //threshold (chance) of dropping the collectible
    [Range(0,100)] public float chanceToDropCollectible;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject ChooseDropCollectible()
    {
        //generate a random number between 0 and 100
        //if it's <= 50, top a cherry, otherwise drop a fire
        float dropNumber = Random.Range(0, 100f);

        if(dropNumber <= 50)
        {
            return collectibleCherry;
        }
        else
        {
            return collectibleFire;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("Hit enemy");

            //deactivate the enemy
            //!!!!! cannot deactivate the other.gameObject only because it would only deactivate the enemy sprite but not the whole object itself !!!!!
            //this is why we need to deactivate the parent object
            other.transform.parent.gameObject.SetActive(false);

            //set enemy death effect
            //Instantiate creates a copy (gameObject passed as first arg, position and rotation as 2nd and 3rd)
            Instantiate(deathEffect, other.transform.position,other.transform.rotation);

            //make the Player bounce off the enemy
            PlayerController.instance.Bounce();

            //drop the collectible if the threshod has been achieved
            float dropChance = Random.Range(0, 100f);

            if(dropChance <= chanceToDropCollectible)
            {
                //drop collectible
                Instantiate(ChooseDropCollectible(), other.transform.position, other.transform.rotation);
            }
        }
    }
}
