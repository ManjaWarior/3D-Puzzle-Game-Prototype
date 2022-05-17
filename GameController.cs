using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{

    private static int floorsTraversed = 0;
    public static int FloorsTraversed { get => floorsTraversed; set => floorsTraversed = value; }


    private static float timeRemaining;
    public static float TimeRemaining { get => timeRemaining; set => timeRemaining = value; }


    private static bool gamePaused = true;
    public static bool GamePaused { get => gamePaused; set => gamePaused = value; }//all 3 of these allows other classes and scripts to access and edit the private variables

    [SerializeField] private GameObject userInterface = null;
    [SerializeField] private GameObject player = null;

    private GameObject[] floors;//array used for storing all floor objects

    public static GameController instance;

    private void Start()
    {
        floors = GameObject.FindGameObjectsWithTag("Floor");//finds all game objects with the tag floor and stores them in an array
        player = GameObject.FindWithTag("Player");//finds the player 
    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!gamePaused)
        {
            if (timeRemaining > 0)
            {
                TimeRemaining -= Time.deltaTime;//reduces time
            }
            else if (timeRemaining <= 0)
            {
                userInterface.GetComponent<UIController>().playerTimeout();//runs the timeout function
                resetFloors();//resets all of the floors
            }
        }
    }


    private void resetFloors()
    {
        player.transform.position = new Vector3(0, 1, 0);//resets player's position
        player.transform.rotation = new Quaternion(0, 0, 0, 0);//resets player's rotation

        for(int i = 0; i < floors.Length; i++)//iterates through each floor to re-enable them
        {
            floors[i].SetActive(true);

            try
            {
                GameObject timeIncrease = floors[i].transform.Find("TimeIncrease").gameObject;//finds any time increase items and stores them for each floor
                timeIncrease.SetActive(true);//re-activates the found time increase item
            }
            catch(NullReferenceException e)
            {
                Debug.Log("No TimeIncrease item on this floor");//if there isn't a time increase item then output this to the log
                e = null;
            }
        }

    }
}
