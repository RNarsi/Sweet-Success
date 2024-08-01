using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
public class FirstPersonControls : MonoBehaviour
{
    // Public variables to set movement and look speed, and the player camera

    public float moveSpeed; // Speed at which the player moves 
    public float lookSpeed; // Sensitivity of the camera movement // looks around
    public float gravity = -9.81f; // Gravity value
    public Transform playerCamera; // Reference to the player's camera // big T - transform of camera ,diff game object , script on player // can refer to any other game objects transform
                                  
    // Private variables to store input values and the character controller                              
    //action map - we put vector 2 
    private Vector2 moveInput; // Stores the movement input from the player
    private Vector2 lookInput; // Stores the look input from the player
    private float verticalLookRotation = 0f; // Keeps track of verticalcamera rotation for clamping // prevent players neck from fliping all the way back
    private Vector3 velocity; // Velocity of the player

    private CharacterController characterController; // Reference to the CharacterController component

    private void Awake()
    {  
        //before any other scripts start method run
        // is for early setup

        // Get and store the CharacterController component attached to this GameObject

        characterController = GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
            //enables input actions
            //listens for player input

            // Create a new instance of the input actions    // "Controls" is the name of the actionmap we created
        var playerInput = new Controls();

        // Enable the input actions
        playerInput.Player.Enable();

        // Subscribe to the movement input events      
                                                                                                 // "Player" is the name action map -we named it Player  
                                                                                                 //Movement is the binding
        playerInput.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); // Update moveInput when movement input is performed 
                                                                                                // Lambda expression(shorter way)
                                                                                                //has the player touched WASD or joystick or arrows? 
                                                                                                //ctx - context, refers to the key bindings
        playerInput.Player.Movement.canceled += ctx => moveInput = Vector2.zero; // Reset moveInput when movement input is canceled
                                                                                  //no input by player - cancels the context that means no movement
      
          // Subscribe to the look input events
                                                                                                // action is LookAround in action map
        playerInput.Player.LookAround.performed += ctx => lookInput = ctx.ReadValue<Vector2>(); // Update lookInput when look input is performed
        playerInput.Player.LookAround.canceled += ctx => lookInput = Vector2.zero; // Reset lookInput when look input is canceled
        

    }
    private void Update()
    {
        // Call Move and LookAround methods every frame to handle player movement and camera rotation

        Move();
        LookAround();
        ApplyGravity();
    }
    public void Move()
    {
        // handles movement based on input

        // Create a movement vector based on the input // 0 is coz we dont want player to float (z axis)
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        // Transform direction from local to world space //translates from local to world space // local - in local context // World- whole scene (world) context 
        // think about planets the moon around earth = local moon around the sun= global
        move = transform.TransformDirection(move);

        // Move the character controller based on the movement vector and speed
        characterController.Move(move * moveSpeed * Time.deltaTime);
    }
    public void LookAround()
    {
        // Get horizontal and vertical look inputs and adjust based on sensitivity

        float LookX = lookInput.x * lookSpeed; //determines how fast move head up, down, left, right
        float LookY = lookInput.y * lookSpeed;

        // Horizontal rotation: Rotate the player object around the y-axis // small t, Why?- transform that this script is on 
        transform.Rotate(0, LookX, 0); // for looking left and right

        // Vertical rotation: Adjust the vertical look rotation and clamp it to prevent flipping
        verticalLookRotation -= LookY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f); // restricting up and down looking
 
        // Apply the clamped vertical rotation to the player camera // attach to camera not player coz we use camera to look around
        playerCamera.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);
        
    }
    public void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0) // check if player grounded / touching floor
        {
            velocity.y = -0.5f; // Small value to keep the player grounded 
        }

        velocity.y += gravity * Time.deltaTime; // Apply gravity to the velocity
        characterController.Move(velocity * Time.deltaTime); // Apply the velocity to the character

    }
    
}