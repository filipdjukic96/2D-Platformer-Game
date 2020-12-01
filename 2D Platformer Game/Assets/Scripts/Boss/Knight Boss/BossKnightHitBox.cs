using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnightHitBox : MonoBehaviour
{
    // PUBLIC //

    public BossKnightController bossController;

    public PolygonCollider2D headPolygonCollider;

    public float timeBetweenHits = 2f;


    // PRIVATE //
    private float timeBetweenHitsCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBetweenHitsCounter > 0)
        {
            timeBetweenHitsCounter -= Time.deltaTime;

            //if enough time has passed, activate this collider
            if(timeBetweenHitsCounter <= 0)
            {
                headPolygonCollider.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the Player has hit the boss AND THE PLAYER IS ABOVE THE BOSS
        if (other.CompareTag("Player"))
        {
            //the Player has hit the boss
            bossController.TakeHit();

            //bounce the Player
            PlayerController.instance.Bounce();

            //deactivate the circle collider around the head
            headPolygonCollider.enabled = false;

            //set timer counter
            timeBetweenHitsCounter = timeBetweenHits;
        }
    }
}
