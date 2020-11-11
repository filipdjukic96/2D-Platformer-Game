using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // PUBLIC //

    public Transform[] points; //points between which the platform will move
    public float moveSpeed; 
    public Transform platform; //actual platform that will move



    // PRIVATE //
    private int currentPointIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move platform to current point
        platform.position = Vector3.MoveTowards(platform.position, points[currentPointIndex].position, moveSpeed * Time.deltaTime);

        //if the platform has reached the current point
        if(Vector3.Distance(platform.position, points[currentPointIndex].position) <= .05f)
        {
            //go to next point
            currentPointIndex = (currentPointIndex + 1) % points.Length;
        }
        
    }
}
