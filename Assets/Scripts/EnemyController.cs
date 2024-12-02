using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    // Enum representing the different states of the AI.
    private enum State
    {
        PatrollingAB,  // Patrolling from point A to point B.
        PatrollingBC,  // Patrolling from point B to point C.
        PatrollingCD,  // Patrolling from point C to point D.
        PatrollingDA,  // Patrolling from point D to point A.
        FollowingPlayer // AI is following the player.
    }

    // The current state of the AI.
    private State currentState;

    // The four patrol points (A, B, C, D).
    public GameObject pointA;
    public GameObject pointB;
    public GameObject pointC;
    public GameObject pointD;

    // The Rigidbody component for the AI (not used directly here, but could be for physics-based movement).
    public Rigidbody rb;

    // The movement speed of the AI.
    public float moveSpeed;

    // Reference to the player (target to follow when detected).
    public Transform player;

    // The distance at which the AI detects the player.
    public float detectionDistance;

    void Start()
    {
        // Start position of the AI is set to point A.
        transform.position = pointA.transform.position;
        // Initial state is set to patrolling from A to B.
        currentState = State.PatrollingAB;
    }

    void Update()
    {
        // Calls the method that handles movement and state transitions based on the current state.
        HandleState();

        // When AI reaches point B, switch to patrolling from B to C and rotate the AI.
        if (transform.position == pointB.transform.position)
        {
            currentState = State.PatrollingBC;
            transform.Rotate(0.0f, 0.0f, -90.0f);  // Rotates AI 90 degrees around the Z-axis.
        }

        // When AI reaches point C, switch to patrolling from C to D and rotate the AI.
        if (transform.position == pointC.transform.position)
        {
            currentState = State.PatrollingCD;
            transform.Rotate(0.0f, 0.0f, -90.0f);  // Rotates AI 90 degrees around the Z-axis.
        }

        // When AI reaches point D, switch to patrolling from D to A and rotate the AI.
        if (transform.position == pointD.transform.position)
        {
            currentState = State.PatrollingDA;
            transform.Rotate(0.0f, 0.0f, -90.0f);  // Rotates AI 90 degrees around the Z-axis.
        }

        // When AI reaches point A, switch to patrolling from A to B and rotate the AI.
        if (transform.position == pointA.transform.position)
        {
            currentState = State.PatrollingAB;
            transform.Rotate(0.0f, 0.0f, -90.0f);  // Rotates AI 90 degrees around the Z-axis.
        }

        // If the player is within detection distance, switch to following the player.
        if (Vector3.Distance(transform.position, player.position) <= detectionDistance)
        {
            currentState = State.FollowingPlayer;
        }
    }

    // Handles the AI's movement based on the current state.
    private void HandleState()
    {
        switch (currentState)
        {
            case State.PatrollingAB:
                // Move the AI towards point B at the specified speed.
                transform.position = Vector3.MoveTowards(transform.position, pointB.transform.position, moveSpeed * Time.deltaTime);
                break;
            case State.PatrollingBC:
                // Move the AI towards point C at the specified speed.
                transform.position = Vector3.MoveTowards(transform.position, pointC.transform.position, moveSpeed * Time.deltaTime);
                break;
            case State.PatrollingCD:
                // Move the AI towards point D at the specified speed.
                transform.position = Vector3.MoveTowards(transform.position, pointD.transform.position, moveSpeed * Time.deltaTime);
                break;
            case State.PatrollingDA:
                // Move the AI towards point A at the specified speed.
                transform.position = Vector3.MoveTowards(transform.position, pointA.transform.position, moveSpeed * Time.deltaTime);
                break;
            case State.FollowingPlayer:
                // Move the AI towards the player position at the specified speed.
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

                // If the AI moves out of detection range, switch back to patrolling.
                if (Vector3.Distance(transform.position, player.position) > detectionDistance)
                {
                    currentState = State.PatrollingAB;
                }
                break;
        }
    }

    // Trigger method for when the AI enters or stays in a trigger collider (such as the player).
    void OnTriggerStay(Collider boom)
    {
        // If the object colliding with the trigger has the tag "Player".
        if (boom.gameObject.CompareTag("Player"))
        {
            // Load the "LostScreen" scene (could represent a game over screen or similar).
            SceneManager.LoadScene("LostScreen");

            // Make the cursor visible and change its lock state.
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
