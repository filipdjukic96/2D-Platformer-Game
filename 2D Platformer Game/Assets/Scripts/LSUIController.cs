using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIController : MonoBehaviour
{
    // PUBLIC //

    public static LSUIController instance;

    //image of the fade screen
    public Image fadeScreenImage;
    public float fadeScreenSpeed;


    // PRIVATE //
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //fade the screen from black to normal at level start
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            //the fade screen is currently clear, make it black
            fadeScreenImage.color = new Color(fadeScreenImage.color.r,
                                              fadeScreenImage.color.g,
                                              fadeScreenImage.color.b,
                                              Mathf.MoveTowards(fadeScreenImage.color.a, 1f, fadeScreenSpeed * Time.deltaTime)); //alpha to 1f by fadeScreenSpeed

            //check if it's faded
            if (fadeScreenImage.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeFromBlack)
        {
            //the screen is faded, return it to normal 

            fadeScreenImage.color = new Color(fadeScreenImage.color.r,
                                              fadeScreenImage.color.g,
                                              fadeScreenImage.color.b,
                                              Mathf.MoveTowards(fadeScreenImage.color.a, 0f, fadeScreenSpeed * Time.deltaTime)); //alpha to 0f by fadeScreenSpeed

            //check if it's faded
            if (fadeScreenImage.color.a == 0f)
            {
                shouldFadeFromBlack = true;
            }
        }
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }
}
