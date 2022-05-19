using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    [SerializeField] private GameObject wholeCurrentFloor = null;//for disabling current floor with Vent
    [SerializeField] private GameObject wholePreviousFloor = null;//for enabling floor above with Jump Pad
    [SerializeField] private GameObject wholeNextFloor = null;//for enabling floor above with Jump Pad
    [SerializeField] private GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");//Finds the player object itself instead of having to manually set for each vent and jump pad
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(tag == "Vent")//checks if jump pad to disable current floor and move player
            {
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 5, player.transform.position.z);//move the player down from it's current position
                wholeCurrentFloor.SetActive(false);//disables the floor the player was on
                wholeNextFloor.SetActive(true);//enables the next floor
                //this means that there will noly be one floor enabled at any one time
                GameController.FloorsTraversed = GameController.FloorsTraversed + 1; // increment the amount of floors completed by 1
                UIController.instance.showFloorMovementText(false);//false for moving down a floor
            }
            if(tag == "Jump Pad")//checks if jump pad to enable previous floor and move player
            {
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5, player.transform.position.z);//move the player up from it's current position
                wholePreviousFloor.SetActive(true);//enables the previous floor
                wholeCurrentFloor.SetActive(false);//disables the floor the player is on
                //this also allows one floor to be enabled at any one time
                GameController.FloorsTraversed = GameController.FloorsTraversed - 1;// decrement the amount of floors completed by 1
                UIController.instance.showFloorMovementText(true);//true for moving up a floor
            }
            if (tag == "Time Increase")//checks if the object collided with is the time increasing item
            {
                int increaseTimeAmount = Random.Range(5, 15);
                GameController.TimeRemaining = GameController.TimeRemaining + increaseTimeAmount;//adds time randomly between 5 seconds and 15 seconds
                gameObject.SetActive(false);//sets the object to false so it cannot be collided with again
                UIController.instance.showTimeIncreaseText(increaseTimeAmount);//display the text with the amoutn of time increased
            }
        }
    }

}
