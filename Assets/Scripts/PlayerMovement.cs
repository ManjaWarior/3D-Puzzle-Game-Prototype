using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    PlayerMovement instance;
    public static Rigidbody rb;
    private float speed = 5f;//speed increased from 3 to 5 to make it a little easier 
    private float rotationSpeed = 15f;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GameController.GamePaused)
        {

            if (Input.GetKey(KeyCode.W))
            {
                //Move the Rigidbody forwards constantly
                rb.velocity = transform.forward * speed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                //Move the Rigidbody backwards constantly
                rb.velocity = -transform.forward * speed;
            }

            if (Input.GetKey(KeyCode.D))
            {
                //Rotate the Rigidbody about the Y axis in the positive direction
                transform.Rotate(new Vector3(0, 5, 0) * Time.deltaTime * rotationSpeed, Space.World);
            }

            if (Input.GetKey(KeyCode.A))
            {
                //Rotate the Rigidbody about the Y axis in the negative direction
                transform.Rotate(new Vector3(0, -5, 0) * Time.deltaTime * rotationSpeed, Space.World);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string arg = other.tag;

        switch (arg)//switch is more efficient than a series of if's, also allows for easier readability, maintainability and expandability 
        {
            case "Water":
                speed = 2.0f;//sets the speed to below half if the player enters the water
                break;
            case "Vent":
                speed = 3.0f;//sets the speed to normal if the player hits a vent, therefore leaving the water
                break;
            case "Jump Pad":
                speed = 3.0f;//sets the speed to normal if the player hits a jump pad, therefore leaving the water
                break;
            case "Speed Boost":
                speed = 7.0f;//sets the speed to 7 instead of 5 as they are in the speed boost zone
                UIController.instance.SpeedLines(true);
                break;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        string arg = other.tag;

        switch (arg)
        {
            case "Water":
                speed = 3.0f;//sets the speed to normal if the player leaves the water
                break;
            case "Speed Boost":
                speed = 3.0f;//sets the speed to normal if the player leaves the speed boost
                UIController.instance.SpeedLines(false);//disables the speed boost particle effects
                break;
        }
    }
}
