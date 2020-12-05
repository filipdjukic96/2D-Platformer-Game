using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : StateMachineBehaviour 
{
    private float timer;
    public float minTime;
    public float maxTime;

    private Transform playerPos;
    public float speed;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        //if a Player exists
        if (GameObject.FindGameObjectsWithTag("Player").Length != 0)
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            playerPos = null;
        }

        timer = Random.Range(minTime, maxTime);
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        if (timer <= 0)
        {
            animator.SetTrigger("idle");
        }
        else 
        {
            timer -= Time.deltaTime;
        }

        //if the Player is dead or non-existent, return
        if (playerPos == null)
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length != 0)
            {
                playerPos = GameObject.FindGameObjectWithTag("Player").transform;
            }
            else
            {
                //return to idle state
                animator.SetTrigger("idle");

                return;
            }
        }


        Vector2 target = new Vector2(playerPos.position.x, animator.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	
	}

}
