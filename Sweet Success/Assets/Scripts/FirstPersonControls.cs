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

    [Header("FRIDGE DOOR SETTINGS")]
    [Space(5)]
    public Transform Hinge;
    private bool Open;
  

    [Header("PLACE SETTINGS")]
    [Space(5)]

    //  private bool HoldingItems = false;
    
    private bool holdingPickUp = false;

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

    [Space(5)]
    public GameObject saltGrainsPrefab; //  prefab for placing
    public Transform saltGrainsSpawnPoint;// Point from which it will spawn
    private bool holdingSalt = false;

    [Space(5)]
    public GameObject flourPowderPrefab; // prefab for placing
    public Transform flourPowderSpawnPoint;//Point from which it will spawn
    private bool holdingFlour = false;

    [Space(5)]
    public GameObject waterDropsPrefab; // prefab for placing
    public Transform waterDropsSpawnPoint;// Point from which it will spawn
    private bool holdingWater = false;

    [Space(5)]
    public GameObject milkDropsPrefab; // prefab for placing
    public Transform milkDropsSpawnPoint;// Point from which it will spawn
    private bool holdingMilk = false;

    [Space(5)]
    public GameObject cookingOilDropsPrefab; // prefab for placing
    public Transform cookingOilDropsSpawnPoint;// Point from which it will spawn
    private bool holdingCookingOil = false;
    
    [Space(5)]
    public GameObject vanillaExtractDropsPrefab; // prefab for placing
    public Transform vanillaExtractDropsSpawnPoint;// Point from which it will spawn
    private bool holdingVanillaExtract = false;

    [Space(5)]
    public GameObject chocoChipsPrefab; // prefab for placing
    public Transform chocoChipsSpawnPoint;// Point from which it will spawn
    private bool holdingChocolateChips = false;

    [Space(5)]
    public GameObject blueberryPrefab; // prefab for placing
    public Transform  blueberrySpawnPoint;// Point from which it will spawn
    private bool holdingBlueberries = false;

    [Space(5)]
    public GameObject bakingSodaPowderPrefab; // prefab for placing
    public Transform bakingSodaPowderSpawnPoint;// Point from which it will spawn
    private bool holdingBakingSoda= false;

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

        playerInput.Player.Interaction.performed += ctx => Interact();
    }
    private void Update()
    {
        // Call Move and LookAround methods every frame to handle player movement and camera rotation

        Move();
        LookAround();
        ApplyGravity();

        if (Open && Hinge.rotation.y < 0.2f)
        {
            Hinge.Rotate(0, 90 * Time.deltaTime, 0);
        }
        else if (Hinge.rotation.y > 0.2f)
        {
            Open = false;
        }
        Debug.Log(Hinge.rotation.y);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.CompareTag("Step"))
        {
            Open = true;
        }
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
            // Instantiate at the spawn point
            GameObject butterBlock = Instantiate(butterBlockPrefab, butterBlockSpawnPoint.position, butterBlockSpawnPoint.rotation);

            // Get the Rigidbody component and set its velocity
            Rigidbody rb = butterBlock.GetComponent<Rigidbody>();
            rb.velocity = butterBlockSpawnPoint.forward * 0.1f;
        }
        if (holdingSugar == true)
        {
            // Instantiate at the spawn point
            GameObject sugarCubes = Instantiate(sugarCubesPrefab, sugarCubesSpawnPoint.position, sugarCubesSpawnPoint.rotation);

            // Get the Rigidbody component and set its velocity
            Rigidbody rb = sugarCubes.GetComponent<Rigidbody>();
            rb.velocity = sugarCubesSpawnPoint.forward * 0.1f;
        }
        if (holdingSalt == true) 
        {
            // Instantiateat the spawn point
            GameObject saltGrains = Instantiate(saltGrainsPrefab, saltGrainsSpawnPoint.position, saltGrainsSpawnPoint.rotation);

            // Get the Rigidbody component and set its velocity
            Rigidbody rb = saltGrains.GetComponent<Rigidbody>();
            rb.velocity = saltGrainsSpawnPoint.forward * 0.1f;
        }
        if (holdingFlour == true) 
        {
           // Instantiate at the spawn point
           GameObject flourPowder = Instantiate(flourPowderPrefab, flourPowderSpawnPoint.position, flourPowderSpawnPoint.rotation);

           // Get the Rigidbody component and set its velocity
           Rigidbody rb = flourPowder.GetComponent<Rigidbody>();
           rb.velocity = flourPowderSpawnPoint.forward * 0.1f;
        }
        if (holdingWater == true) 
        {
           // Instantiate at the spawn point
           GameObject waterDrops = Instantiate(waterDropsPrefab, waterDropsSpawnPoint.position, waterDropsSpawnPoint.rotation);

           // Get the Rigidbody component and set its velocity
           Rigidbody rb = waterDrops.GetComponent<Rigidbody>();
           rb.velocity = waterDropsSpawnPoint.forward * 0.1f;
        }
        if (holdingMilk == true)
        {
            // Instantiate at the spawn point
            GameObject milkDrops = Instantiate(milkDropsPrefab, milkDropsSpawnPoint.position, milkDropsSpawnPoint.rotation);

            // Get the Rigidbody component and set its velocity
            Rigidbody rb = milkDrops.GetComponent<Rigidbody>();
            rb.velocity = milkDropsSpawnPoint.forward * 0.1f;
        }
        if (holdingBakingSoda == true) 
        {
           // Instantiate at the spawn point
           GameObject bakingSodaPowder = Instantiate(bakingSodaPowderPrefab, bakingSodaPowderSpawnPoint.position, bakingSodaPowderSpawnPoint.rotation);

           // Get the Rigidbody component and set its velocity
           Rigidbody rb = bakingSodaPowder.GetComponent<Rigidbody>();
           rb.velocity = bakingSodaPowderSpawnPoint.forward * 0.1f;
        }
        if (holdingCookingOil == true) 
        {
           // Instantiate at the spawn point
           GameObject cookingOilDrops = Instantiate(cookingOilDropsPrefab, cookingOilDropsSpawnPoint.position, cookingOilDropsSpawnPoint.rotation);

           // Get the Rigidbody component and set its velocity
           Rigidbody rb = cookingOilDrops.GetComponent<Rigidbody>();
           rb.velocity = cookingOilDropsSpawnPoint.forward * 0.1f;
        }
        if (holdingVanillaExtract == true) 
        {
          // Instantiate at the spawn point
          GameObject vanillaExtractDrops = Instantiate(vanillaExtractDropsPrefab, vanillaExtractDropsSpawnPoint.position, vanillaExtractDropsSpawnPoint.rotation);

          // Get the Rigidbody component and set its velocity
          Rigidbody rb = vanillaExtractDrops.GetComponent<Rigidbody>();
          rb.velocity = vanillaExtractDropsSpawnPoint.forward * 0.1f;
        }
        if (holdingChocolateChips == true) 
        {
           // Instantiate at the spawn point
           GameObject chocoChips = Instantiate(chocoChipsPrefab, chocoChipsSpawnPoint.position, chocoChipsSpawnPoint.rotation);

           // Get the Rigidbody component and set its velocity
           Rigidbody rb = chocoChips.GetComponent<Rigidbody>();
           rb.velocity = chocoChipsSpawnPoint.forward * 0.1f;
        }
        if (holdingBlueberries == true) 
        {
          // Instantiate at the spawn point
          GameObject blueberry = Instantiate(blueberryPrefab, blueberrySpawnPoint.position, blueberrySpawnPoint.rotation);

          // Get the Rigidbody component and set its velocity
          Rigidbody rb = blueberry.GetComponent<Rigidbody>();
          rb.velocity = blueberrySpawnPoint.forward * 0.1f;
        }
        else
        {
          holdingPickUp = true;
        }
    }

    public void Interact()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        Debug.DrawRay(playerCamera.position, playerCamera.forward * pickUpRange, Color.red, 2f); 

        if (Physics.Raycast(ray,out hit, pickUpRange))
        {
            hit.collider.CompareTag("Door");
        }
        if (hit.collider.CompareTag("Door"))
        {
            StartCoroutine(SlideDoor(hit.collider.gameObject));
        }
    }
   

    private IEnumerator SlideDoor(GameObject door)
    {
        float openAmount = 5f; 
        float openSpeed = 2f; 
        Vector3 startPosition = door.transform.position;  
        Vector3 endPosition = startPosition + Vector3.right * openAmount; 

       
        while (door.transform.position.x < endPosition.x)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, endPosition, openSpeed * Time.deltaTime);
            yield return null; //Wait until the next frame before continuing the loop
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
                holdingPickUp = false;
                holdingMilk = false;
                holdingFlour = false;
                holdingWater = false;
                holdingBakingSoda = false;
                holdingCookingOil = false; 
                holdingVanillaExtract = false;
                holdingChocolateChips = false;
                holdingBlueberries = false;

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
                holdingPickUp = false;
                holdingMilk = false;
                holdingFlour =false;
                holdingWater=false;
                holdingBakingSoda=false;
                holdingCookingOil=false;
                holdingVanillaExtract=false;
                holdingChocolateChips=false;
                holdingBlueberries=false; 
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
                holdingPickUp=false;
                holdingMilk = false;
                holdingFlour =false;
                holdingWater=false;
                holdingBakingSoda=false;
                holdingCookingOil=false;
                holdingVanillaExtract=false;
                holdingChocolateChips=false;
                holdingBlueberries=false;

            }
            if (hit.collider.CompareTag("Salt"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;

                holdingSalt = true;
                holdingEgg = false;
                holdingButter = false;
                holdingSugar = false;
                holdingFlour = false;
                holdingWater = false;
                holdingMilk = false;
                holdingBakingSoda = false;
                holdingCookingOil = false;
                holdingVanillaExtract = false;
                holdingChocolateChips = false;
                holdingBlueberries = false;
                holdingPickUp = false;

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

                holdingFlour = true;
                holdingEgg = false;
                holdingButter = false;
                holdingSugar = false;
                holdingSalt = false;
                holdingWater = false;
                holdingMilk = false;
                holdingBakingSoda = false;
                holdingCookingOil = false;
                holdingVanillaExtract = false;
                holdingChocolateChips = false;
                holdingBlueberries = false;
                holdingPickUp = false;

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

                holdingWater = true;
                holdingEgg = false;
                holdingButter = false;
                holdingSugar = false; 
                holdingSalt = false;
                holdingFlour = false;
                holdingMilk = false;
                holdingBakingSoda = false;
                holdingCookingOil = false;
                holdingChocolateChips = false;
                holdingBlueberries = false;
                holdingPickUp = false;

            }
            if (hit.collider.CompareTag("Milk"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;

                holdingMilk = true;
                holdingEgg = false;
                holdingButter = false;
                holdingSugar = false;
                holdingSalt = false;
                holdingFlour = false;
                holdingWater = false;
                holdingBakingSoda = false;
                holdingCookingOil = false;
                holdingChocolateChips = false;
                holdingBlueberries = false;
                holdingPickUp = false;

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

                holdingBakingSoda = true;
                holdingEgg = false;
                holdingButter = false;
                holdingSugar = false;
                holdingSalt = false;
                holdingFlour = false;
                holdingWater = false;
                holdingMilk = false;
                holdingCookingOil = false;
                holdingVanillaExtract = false;
                holdingChocolateChips = false;
                holdingBlueberries = false;
                holdingPickUp = false;

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

                holdingCookingOil = true;
                holdingEgg = false;
                holdingButter = false;
                holdingSugar = false;
                holdingSalt = false;
                holdingFlour = false;
                holdingWater = false;
                holdingMilk = false;
                holdingBakingSoda = false;
                holdingVanillaExtract = false;
                holdingChocolateChips = false;
                holdingBlueberries = false;
                holdingPickUp = false;

            }
            if (hit.collider.CompareTag("Vanilla Extract"))
            {
               // Pick up the object
               heldObject = hit.collider.gameObject;
               heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

               // Attach the object to the hold position
               heldObject.transform.position = holdPosition.position;
               heldObject.transform.rotation = holdPosition.rotation;
               heldObject.transform.parent = holdPosition;

                holdingVanillaExtract = true;
                holdingEgg = false;
                holdingButter = false;
                holdingSugar = false;
                holdingSalt = false;
                holdingFlour = false;
                holdingWater = false;
                holdingMilk = false;
                holdingBakingSoda = false;
                holdingCookingOil = false;
                holdingChocolateChips = false;
                holdingBlueberries = false;
                holdingPickUp = false;

            }
            if (hit.collider.CompareTag("Chocolate Chips"))
            {
               // Pick up the object
               heldObject = hit.collider.gameObject;
               heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

               // Attach the object to the hold position
               heldObject.transform.position = holdPosition.position;
               heldObject.transform.rotation = holdPosition.rotation;
               heldObject.transform.parent = holdPosition;

                holdingChocolateChips = true;
                holdingEgg = false;
                holdingButter = false;
                holdingSugar = false;
                holdingSalt = false;
                holdingFlour = false;
                holdingWater = false;
                holdingMilk = false;
                holdingBakingSoda = false;
                holdingCookingOil = false;
                holdingVanillaExtract = false;
                holdingBlueberries = false;
                holdingPickUp = false;

            }
            if (hit.collider.CompareTag("Blueberries"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;

                holdingBlueberries = true;
                holdingEgg = false;
                holdingButter = false;
                holdingSugar = false;
                holdingSalt = false;
                holdingFlour = false;
                holdingWater = false;
                holdingMilk = false;
                holdingBakingSoda = false;
                holdingCookingOil = false;
                holdingVanillaExtract = false;
                holdingChocolateChips = false;
                holdingPickUp = false;

            }
            if (hit.collider.CompareTag("PickUp"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;

                holdingPickUp = true;
                holdingEgg = false;
                holdingButter = false;
                holdingSugar = false;
                holdingFlour = false;
                holdingWater = false;
                holdingMilk = false;
                holdingBakingSoda = false;
                holdingCookingOil = false;
                holdingVanillaExtract = false;
                holdingChocolateChips = false;
                holdingBlueberries = false;
            }
        }
    }


}