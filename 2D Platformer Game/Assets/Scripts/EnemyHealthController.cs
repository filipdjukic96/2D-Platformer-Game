using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    // PUBLIC //


    //health
    public int currentHealth;
    public int maxHealth;
    //death effect of the enemy
    public GameObject deathEffect;
    //collectible which will be dropped (cherry or fire)
    public GameObject collectibleCherry;
    public GameObject collectibleFire;
    //threshold (chance) of dropping the collectible
    [Range(0, 100)] public float chanceToDropCollectible;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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

        if (dropNumber <= 50)
        {
            return collectibleCherry;
        }
        else
        {
            return collectibleFire;
        }
    }

    public void DamageEnemy()
    {
        currentHealth--;

        if (currentHealth == 0)
        {
            //destroy enemy
            //set object as false
            gameObject.SetActive(false);
            //instantiate a death effect
            //Instantiate creates a copy (gameObject passed as first arg, position and rotation as 2nd and 3rd)
            Instantiate(deathEffect, transform.position, transform.rotation);

            //play enemy destroyed sound effect
            AudioManager.instance.PlaySFX(AudioManager.SoundEffects.EnemyExplode);

            //drop the collectible if the threshod has been achieved
            float dropChance = Random.Range(0, 100f);

            if (dropChance <= chanceToDropCollectible)
            {
                //drop collectible
                Instantiate(ChooseDropCollectible(), transform.position, transform.rotation);
            }
        }
    }
}
