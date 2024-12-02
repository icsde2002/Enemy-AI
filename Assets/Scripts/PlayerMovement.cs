using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f;//adjust the movment speed.
    public float mousensivity = 2.0f;//adjust the mouse sensivity.
    private float verticalRotation = 0;//ajust the vertical rotation.
    private Rigidbody rb;//checking the physics of the character.
    // Start is called before the first frame update.
    void Start()
    {
        rb = GetComponent<Rigidbody>();//grabs the rigid body of the object.
        Cursor.lockState = CursorLockMode.Locked;//locks the cursor.
    }
    void Update()
    {
        RotationInputM();
        WASD();
        // Check if the Escape key is pressed.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Call the ExitButton method to play the sound and quit the application.
            Application.Quit();
        }
    }
    void RotationInputM()
    {
        float horizontalRotation = Input.GetAxis("Mouse X") * mousensivity;//grabs the horizontal rotation of the player and makes an and xyz mouse sensivity. 
        transform.Rotate(0, 0, horizontalRotation);//assining it to rotation on an object.
        verticalRotation -= Input.GetAxis("Mouse Y") * mousensivity;//bgrabs the verticalrotation and revers the mouse input.
        verticalRotation = Mathf.Clamp(0, 0, verticalRotation);//uses clamp to limit your rotation so it won't exceed more than +-90.
    }
    void WASD()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");//takes the horizontal input from wasd and arrows key.
        float moveVertical = Input.GetAxis("Vertical");//takes the vertical input from wasd and arrow keys.
        Vector3 movement = new Vector3(-moveVertical, -moveHorizontal, 0) * movementSpeed * Time.deltaTime;//multiples that value butmovspseed and time.
        movement = transform.TransformDirection(movement);//makes sure direction is correct.
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);//sets the velocity only to the rigidbody.
    }
}