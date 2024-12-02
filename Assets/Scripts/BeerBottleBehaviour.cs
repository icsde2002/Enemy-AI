using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoalMovement : MonoBehaviour
{
    // How fast it moves up and down.
    public float moveSpeed = 1.0f;
    // How high it moves up and down.
    public float amplitude = 0.5f;
    // Where It Will Spawn.
    private Vector3 startPos;
    // Adjust the speed of the rotation in degrees per second in the Inspector.
    public float rotationSpeed = 60f;
    // once the script starts.
    void Start()
    {
        // Store the initial position of the object.
        startPos = transform.position;
    }
    // Once per frame.
    void Update()
    {
        // Matematics calculations for vertical movement.
        float verticalMovement = Mathf.Sin(Time.time * moveSpeed) * amplitude;

        // Rotate the object around its axis continuously.
        transform.rotation = Quaternion.Euler(-45f, transform.rotation.eulerAngles.y + rotationSpeed * Time.deltaTime, 0f);

        // Updating the object's position by adding the vertical movement to its initial position.
        Vector3 newPosition = startPos + Vector3.up * verticalMovement;
        transform.position = newPosition;
    }
    void OnTriggerStay(Collider boow)
    {
        // Check if it collides with the player.
        if (boow.gameObject.CompareTag("Player"))
        {
            // if it collides with the player, then change the scene to "WinScreen".
            SceneManager.LoadScene("WinScreen");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    } 
}