using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //PUBLIC//

    //Transform component of the object the camera will follow
    public Transform target;

    //reference to the Transform component of the far background
    public Transform farBackground;
    //reference to the Transfrom component of the middle background
    public Transform middleBackground;

    //min and max height for the Main Camera
    public float minHeight, maxHeight;

    //PRIVATE//
    private float lastXPosition; //last x position of the Main Camera's Transform component
    private float lastYPosition; //last y position of the Main Camera's Transform component

    // Start is called before the first frame update
    void Start()
    {
        lastXPosition = transform.position.x;
        lastYPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //transform is the Transform component of the object this script is attached to (Main Camera)
        //following the target on X axis and on Y axis (between minHeight and maxHeight)
        //Mathf.Clamp assures the first parameter value is between min and max
        transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);

        //amount how much the x has moved since last frame update
        float amountToMoveX = transform.position.x - lastXPosition;
        //update last x position
        lastXPosition = transform.position.x;

        //amount how much the y has moved since last frame update
        float amountToMoveY = transform.position.y - lastYPosition;
        //update last y position
        lastYPosition = transform.position.y;

        
        //move far background at the same position as the Main Camera (only X axis!)
        farBackground.position = farBackground.position + new Vector3(amountToMoveX, amountToMoveY, 0f);
        //move middle background along the X axis at 1/2 of amountToMoveX (for depth)
        middleBackground.position = middleBackground.position + new Vector3(amountToMoveX * .5f, amountToMoveY * .5f, 0);
    }
}
