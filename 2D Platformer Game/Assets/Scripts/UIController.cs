using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    //PUBLIC//

    public static UIController instance;

    //Images for hearts
    public Image heart1, heart2, heart3;
    //full heart sprite
    public Sprite heartFull;
    //empty heart sprite
    public Sprite heartEmpty;
    //half heart sprite
    public Sprite heartHalf;

    //image of the fade screen
    public Image fadeScreenImage;
    public float fadeScreenSpeed;
    

    //gem score
    public Text gemScoreText;
    //fire score
    public Text fireScoreText;
    
    
    //level complete text
    public GameObject levelCompleteText;


    // PRIVATE //
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        //set UI for gems and fires (0)
        UpdateGemScoreDisplay();
        UpdateFireScoreDisplay();

        //fade the screen from black to normal at level start
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldFadeToBlack)
        {
            //the fade screen is currently clear, make it black
            fadeScreenImage.color = new Color(fadeScreenImage.color.r, 
                                              fadeScreenImage.color.g, 
                                              fadeScreenImage.color.b, 
                                              Mathf.MoveTowards(fadeScreenImage.color.a, 1f, fadeScreenSpeed * Time.deltaTime)); //alpha to 1f by fadeScreenSpeed
            
            //check if it's faded
            if(fadeScreenImage.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }            
        }

       if(shouldFadeFromBlack)
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

    public void UpdateHealthDisplay()
    {
        switch(PlayerHealthController.instance.currentHealth)
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;

            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                break;

            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                break;

            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                break;

            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;

            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;

            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
        }
    }


    public void UpdateGemScoreDisplay()
    {
        gemScoreText.text = LevelManager.instance.gemScoreCollected.ToString();
    }

    public void UpdateFireScoreDisplay()
    {
        fireScoreText.text = LevelManager.instance.fireScoreCollected.ToString();
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
