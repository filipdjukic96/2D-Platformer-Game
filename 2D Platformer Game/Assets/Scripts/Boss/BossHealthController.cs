using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthController : MonoBehaviour
{
    // PUBLIC //

    public int health = 5;

    public GameObject explosion; //explosion the the boss is defeated

    public GameObject winPlatform; //platform to be shown after the boss is defeated


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeHit()
    {
        health--;

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

        //instantiate an explosion
        Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);

        //activate the win platform
        winPlatform.SetActive(true);

        //stop boss music
        AudioManager.instance.StopBossMusic();
    }
}
