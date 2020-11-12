using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    // PUBLIC //

    public float bounceForce = 20f; //force to fling the Player

    // PRIVATE //

    private Animator animator; //animator of the Bouncer

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            //change the Player velocity Y component to bounceForce
            PlayerController.instance.rigidBody.velocity = new Vector2(PlayerController.instance.rigidBody.velocity.x, bounceForce);

            //set trigger for animation
            animator.SetTrigger("Bounce");
        }
    }
}
