using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // PUBLIC //
    public string startScene; //first scene in the game (when the user clicks on start)

    public string continueScene; //continue last played 

    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        //check if the first level is unlocked
        //if it is, make the CONTINUE button visible
        if(PlayerPrefs.HasKey(startScene + "_unlocked"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            //otherwise, the game hasn't been played before, set the CONTINUE button to invisible
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);

        //delete all entries in player prefs
        PlayerPrefs.DeleteAll();
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(continueScene);
    }

    public void QuitGame()
    {
        Application.Quit(); //doesn't work in Unity editor
    }
}
