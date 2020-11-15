using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stompbox is a Box Collider used for detecting when the Player stomps on an enemy from above

public class Stompbox : MonoBehaviour
{

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
        if(other.tag == "Enemy")
        {
            Debug.Log("Hit enemy");

            //DAMAGE ENEMY

            // the Box Collider 2D collided with the Box Collider of the enemy sprite

            //if other's parent is != null, that means we collided with the enemy sprite
            if (other.transform.parent != null)
            {
                //!!!!! cannot damage the other.gameObject only because it would only damage the enemy sprite but not the whole object itself !!!!!
                //this is why we need to damage the parent object

                //if it has an EnemyController attached, it's a real enemy
                if(other.transform.parent.gameObject.TryGetComponent<EnemyHealthController>(out var enemyHealthController))
                {
                    Debug.Log("Hit frog or opossum enemy");
                    enemyHealthController.DamageEnemy();
                }
                else
                {
                    Debug.Log("Enemy has no health controller");
                }
               

            }

            //make the Player bounce off the enemy
            PlayerController.instance.Bounce();
        }
    }
}
