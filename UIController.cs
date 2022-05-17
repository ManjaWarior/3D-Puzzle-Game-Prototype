using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu = null;//stores the main menu game object
    [SerializeField] private GameObject restartMenu = null;//restart menu gmae object
    [SerializeField] private GameObject controlScreen = null;//controls screen game object
    [SerializeField] private GameObject inGameUI = null;//in game UI game object

    [SerializeField] private TMP_Text floorsTraversedText = null;//text to display how many floors have been traversed
    [SerializeField] private TMP_Text restartMenuFloorsTraversedText = null;//text to display how many floors have been traversed
    [SerializeField] private TMP_Text timeRemainingText = null;//text to display the remaining time
    [SerializeField] private TMP_Text floatingText = null;//text to show how much a time increase item increased the timer by

    [SerializeField] private Animator floatingTextFade = null; //animation to fade out time increase text

    [SerializeField] private ParticleSystem speedLines = null;//Speed lines for when player is on a jump pad

    public static UIController instance;

    //everything is done in functions so they can be called by other scripts or used when buttons are pressed
    void Awake()
    {
        instance = this;
        GameController.TimeRemaining = 120;
    }

    public void startGame()
    {
        mainMenu.SetActive(false);
        inGameUI.SetActive(true);
        GameController.GamePaused = false;
        GameController.TimeRemaining = 120;//initial start time //make sure is set to 120 for final build
    }

    public void restartGame()
    {
        restartMenu.SetActive(false);
        inGameUI.SetActive(true);
        GameController.GamePaused = false;
        GameController.TimeRemaining = 120;//resets everything and disabled the menu and re-activates the in game UI elements
    }

    public void controlsMenu()
    {
        controlScreen.SetActive(true);//overlays the control screen on top
    }

    public void returnToMenu()
    {
        controlScreen.SetActive(false);//removes controls screen
        restartMenu.SetActive(false);//removes restart screen, if already disabled won't do anything
        mainMenu.SetActive(true);//re-activates the main menu if it was disabled
    }

    public void playerTimeout()
    {
        inGameUI.SetActive(false);//disables the in-game text UI elements
        restartMenu.SetActive(true);//opens the restart panel
        restartMenuFloorsTraversedText.text = "Floors Traversed: " + GameController.FloorsTraversed;//must be done before reset so the number of floors is accurate
        GameController.GamePaused = true;//stops the game functions from carrying on after the player is dead
        GameController.FloorsTraversed = 0;
        GameController.TimeRemaining = 0;//resets all variables when the player dies
    }

    public void endGame()
    {
        Application.Quit();
    }

    public void showTimeIncreaseText(int timeIncreased)
    {
        floatingText.gameObject.SetActive(true);
        floatingTextFade.Play("floatingTextFade", 0);//plays the animation
        floatingText.text = timeIncreased.ToString() + " Seconds added";//displays the number of seconds added to the player
        Invoke("hideText", 2.5f);//this will call the hideText function after 2.5 seconds
    }

    public void showFloorMovementText(bool moveUp)
    {
        floatingText.gameObject.SetActive(true);
        floatingTextFade.Play("floatingTextFade", 0);
        if(moveUp == true)//jump pad
        {
            floatingText.text = "Moved up a floor";
        }
        else if (moveUp == false)//vent
        {
            floatingText.text = "Moved down a floor";
        }
        Invoke("hideText", 2.5f);
    }

    void hideText()
    {
        floatingText.gameObject.SetActive(false);//disables the text so it isn't visible
    }

    public void SpeedLines(bool active)
    {
        speedLines.gameObject.SetActive(active);//speedLines will only be visible when the player is on a speed boost
    }

    public void FixedUpdate()
    {
        if(inGameUI.activeSelf == true)//only updates the floor text and time text when the UI is active for it
        {
            floorsTraversedText.text = "Floors Traversed: " + GameController.FloorsTraversed.ToString();//converts integer to string for the text
            int timeRemaining = (int)GameController.TimeRemaining;//converts to int so it is a whole number
            timeRemainingText.text = "Time Remaining: " + timeRemaining.ToString();//convers the integer to string so it can be displayed
        }
    }
}
