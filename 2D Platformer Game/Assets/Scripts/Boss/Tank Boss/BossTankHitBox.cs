using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankHitBox : MonoBehaviour
{
    public BossTankController bossController;

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
        //the Player has hit the boss with a bullet
        bossController.TakeHit();

        //deactivate this hit box until the boss is moved
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the Player has hit the boss AND THE PLAYER IS ABOVE THE BOSS
        if (other.CompareTag("Player") && PlayerController.instance.transform.position.y > transform.position.y)
        {
            //the Player has hit the boss
            bossController.TakeHit();

            //bounce the Player
            PlayerController.instance.Bounce();

            //deactivate this hit box until the boss is moved
            gameObject.SetActive(false);
        }
    }

}
