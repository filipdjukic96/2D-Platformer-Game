using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //automatically called by Unity when the trigger is activated 
    //(when another object has enterted the trigger area of the object this DamagePlayer script is attached to)
    private void OnTriggerEnter2D(Collider2D other)
    {
        //check if player has triggered the area (any object with Collider2D can trigger: ground etc.)
        //using Unity's TAG SYSTEM (Player is assigned a 'Player' tag)
        if(other.tag == "Player")
        {
            //sending message to console
            //Debug.Log("HIT");

            //damage player (call DamagePlayer() through PlayerHealthController instance)
            PlayerHealthController.instance.DamagePlayer();

        }

        
    }
}
