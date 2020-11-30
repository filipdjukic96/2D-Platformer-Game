using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    //reference to the boss battle object
    public GameObject theBossBattle;



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
        if(other.CompareTag("Player"))
        {
            //activate the boss battle
            theBossBattle.SetActive(true);

            //deactivate this object
            gameObject.SetActive(false);

            //play boss music
            AudioManager.instance.PlayBossMusic();
        }
    }
}
