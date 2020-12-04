using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    // PUBLIC //

    public int health = 5;

    public GameObject explosion; //explosion the the boss is defeated

    public GameObject winPlatform; //platform to be shown after the boss is defeated

    public Slider healthBar;


    // Start is called before the first frame update
    void Start()
    {
        //activate the health bar UI
        healthBar.gameObject.SetActive(true);

        //set health bar's max value to boss' max health
        healthBar.maxValue = health;

        //set current health bar's value to boss' starting health
        healthBar.value = health;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeHit()
    {
        health--;

        //update UI
        healthBar.value = health;

        if(health <= 0)
        {
            Die();
        }

    }

    public int CurrentHealth()
    {
        return health;
    }

    private void Die()
    {
        //deactivate the boss
        gameObject.SetActive(false);

        //deactivate the health bar UI
        healthBar.gameObject.SetActive(false);

        //instantiate an explosion
        Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);

        //activate the win platform
        winPlatform.SetActive(true);

        //stop boss music
        AudioManager.instance.StopBossMusic();
    }
}
