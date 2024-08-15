using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonControls : MonoBehaviour
{
    [Header("MOVEMENT SETTINGS")]
    [Space(5)]
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
    
    [Header("PICKING UP SETTINGS")]
    [Space(5)]
    public Transform holdPosition; // Position where the picked-up object will be held
    private GameObject heldObject; // Reference to the currently held object
    public float pickUpRange = 3f; // Range within which objects can be picked up

    [Header("PLACE SETTINGS")]
    [Space(5)]

  //  private bool HoldingItems = false;

    public GameObject crackedEggPrefab; // crackedEgg prefab for placing
    public Transform crackedEggSpawnPoint; // Point from which the crackedEgg will spawn
    private bool holdingEgg = false;

    [Space(5)]
    public GameObject butterBlockPrefab; // butterBlock prefab for placing
    public Transform butterBlockSpawnPoint;// Point from which the butterBlock will spawn
    private bool holdingButter = false;

    [Space(5)]
    public GameObject sugarCubesPrefab; // sugarCubes prefab for placing
    public Transform sugarCubesSpawnPoint;// Point from which the sugarCubes will spawn
    private bool holdingSugar = false;


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

        // Subscribe to the Place input event
        playerInput.Player.Place.performed += ctx => Place(); // Call the Place method when place input is performed

        // Subscribe to the pick-up input event
        playerInput.Player.PickUp.performed += ctx => PickUpObject(); // Call the PickUpObject method when pick-up input is performed

    

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
        ///yes

    }


    public void Place()
    {
        if (holdingEgg == true)
        {
            // Instantiate the crackedEgg at the spawn point
            GameObject crackedEgg = Instantiate(crackedEggPrefab, crackedEggSpawnPoint.position, crackedEggSpawnPoint.rotation);

            // Get the Rigidbody component and set its velocity
            Rigidbody rb = crackedEgg.GetComponent<Rigidbody>();
            rb.velocity = crackedEggSpawnPoint.forward * 0.1f;

            // Destroy the projectile after 3 seconds
            // Destroy(crackedEgg, 3f);
        }
         if (holdingButter == true)
        {
            // Instantiate the butterBlock at the spawn point
            GameObject butterBlock = Instantiate(butterBlockPrefab, butterBlockSpawnPoint.position, butterBlockSpawnPoint.rotation);

            // Get the Rigidbody component and set its velocity
            Rigidbody rb = butterBlock.GetComponent<Rigidbody>();
            rb.velocity = butterBlockSpawnPoint.forward * 0.1f;
        }
         if (holdingSugar == true) 
        {
            // Instantiate the butterBlock at the spawn point
            GameObject sugarCubes = Instantiate(sugarCubesPrefab, sugarCubesSpawnPoint.position, sugarCubesSpawnPoint.rotation);

            // Get the Rigidbody component and set its velocity
            Rigidbody rb = sugarCubes.GetComponent<Rigidbody>();
            rb.velocity = sugarCubesSpawnPoint.forward * 0.1f;
        }
        
            
        
    }


    public void PickUpObject()
    {
        // Check if we are already holding an object
        if (heldObject != null)
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false; // Enable physics
            heldObject.transform.parent = null;
           
        }

        // Perform a raycast from the camera's position forward
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        // Debugging: Draw the ray in the Scene view: so you can see if it hitting the thing you want it to hit
        Debug.DrawRay(playerCamera.position, playerCamera.forward * pickUpRange, Color.red, 2f); //2 here is "duration"


        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            // Check if the hit object has the tag "PickUp"
            if (hit.collider.CompareTag("Egg"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;

                holdingEgg = true;
                holdingButter = false;
                holdingSugar = false;

            }
            if (hit.collider.CompareTag("Butter"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;

               /* foreach (bool holdingThing in HoldingThingsArray) 
                {
                    holdingThing = false;
                }

                holdingButter = true;     */

                holdingButter = true;
                holdingSugar = false;
                holdingEgg = false;
            }
  
            if (hit.collider.CompareTag("Sugar"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;

                holdingSugar = true;
                holdingEgg = false;
                holdingButter = false;
            }


            /*         if (hit.collider.CompareTag("Salt"))
                     {
                         // Pick up the object
                         heldObject = hit.collider.gameObject;
                         heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                         // Attach the object to the hold position
                         heldObject.transform.position = holdPosition.position;
                         heldObject.transform.rotation = holdPosition.rotation;
                         heldObject.transform.parent = holdPosition;
                     }
                         if (hit.collider.CompareTag("Flour"))
                     {
                         // Pick up the object
                         heldObject = hit.collider.gameObject;
                         heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                         // Attach the object to the hold position
                         heldObject.transform.position = holdPosition.position;
                         heldObject.transform.rotation = holdPosition.rotation;
                         heldObject.transform.parent = holdPosition;
                     }
                     if (hit.collider.CompareTag("Water"))
                     {
                         // Pick up the object
                         heldObject = hit.collider.gameObject;
                         heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                         // Attach the object to the hold position
                         heldObject.transform.position = holdPosition.position;
                         heldObject.transform.rotation = holdPosition.rotation;
                         heldObject.transform.parent = holdPosition;
                     }
                     if (hit.collider.CompareTag("Baking Soda"))
                     {
                         // Pick up the object
                         heldObject = hit.collider.gameObject;
                         heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                         // Attach the object to the hold position
                         heldObject.transform.position = holdPosition.position;
                         heldObject.transform.rotation = holdPosition.rotation;
                         heldObject.transform.parent = holdPosition;
                     }
                     if (hit.collider.CompareTag("Cooking Oil"))
                     {
                         // Pick up the object
                         heldObject = hit.collider.gameObject;
                         heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                         // Attach the object to the hold position
                         heldObject.transform.position = holdPosition.position;
                         heldObject.transform.rotation = holdPosition.rotation;
                         heldObject.transform.parent = holdPosition;
                     }
            */

        }
    }


}