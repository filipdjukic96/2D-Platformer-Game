using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slammer : MonoBehaviour
{
    // PUBLIC //

    //smasher object
    public Transform smasher;

    //slam target
    public Transform slamTarget;

    //slam speed
    public float slamSpeed;

    //distance from the Player at which the slammer will begin slamming
    public float distanceToSlam;

    //time to wait after slam
    public float waitTimeAfterSlam;

    //reset speed
    public float resetSpeed;


    // PRIVATE // 

    //counter to wait after slam
    private float waitTimeCounter;

    //denotes if the slammer should slam
    private bool shouldSlam;

    //denotes if the slammer has slammed
    private bool hasSlammed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(waitTimeCounter > 0)
        {
            waitTimeCounter -= Time.deltaTime;
        }
        else
        {

           if(!hasSlammed)
           {
                //if the player has approached the slam target close enough
                //allow the smasher to drop
                if (Vector3.Distance(slamTarget.position, PlayerController.instance.transform.position) < distanceToSlam)
                {
                    shouldSlam = true;
                    
                }

                if(shouldSlam)
                {
                    //update smasher position
                    smasher.position = Vector3.MoveTowards(smasher.position, slamTarget.position, slamSpeed * Time.deltaTime);

                    //if the smasher has reached the target (ground)
                    if (Vector3.Distance(smasher.position, slamTarget.position) < .05f)
                    {
                        //set wait counter
                        waitTimeCounter = waitTimeAfterSlam;

                        //reset should slam flag
                        shouldSlam = false;

                        //slammer has slammed to the ground
                        hasSlammed = true;
                    }
                }

           }
           else
           {

                //slammer has slammed, move smasher object to original position
                smasher.position = Vector3.MoveTowards(smasher.position, gameObject.transform.position, resetSpeed * Time.deltaTime);

                //check if the smasher is at original position
                if(Vector3.Distance(smasher.position, gameObject.transform.position) < .05f)
                {
                    //reset has slammed flag, allow the smasher to hit the ground again
                    hasSlammed = false;
                }
           }

        }

    }
}
