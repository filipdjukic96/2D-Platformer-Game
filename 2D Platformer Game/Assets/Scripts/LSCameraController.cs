using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSCameraController : MonoBehaviour
{
    // PUBLIC //

    //min and max position
    public Vector2 minPos, maxPos;

    //target the camera is moving towards
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // LateUpdate is called after the Update
    // execute Camera movement in LateUpdate so it always moves AFTER the Player has moved
    void LateUpdate()
    {
        //clamp the X position between minPos and maxPos
        float xPosition = Mathf.Clamp(target.position.x, minPos.x, maxPos.x);

        //clamp the Y position between minPos and maxPos
        float yPosition = Mathf.Clamp(target.position.y, minPos.y, maxPos.y);

        //move the Camera
        transform.position = new Vector3(xPosition, yPosition, transform.position.z);
    }
}
