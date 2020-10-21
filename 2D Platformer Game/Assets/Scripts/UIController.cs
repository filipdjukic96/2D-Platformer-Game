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


    //gem score
    public Text gemScoreText;
    //fire score
    public Text fireScoreText;

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
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
