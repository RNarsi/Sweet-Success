using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

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

    [Header("UI SETTINGS")]
    [Space(5)]
    public TextMeshProUGUI pickUpText;

    [Header("PICKING UP SETTINGS")]
    [Space(5)]
    public Transform holdPosition; // Position where the picked-up object will be held
    private GameObject heldObject; // Reference to the currently held object
    public float pickUpRange = 3f; // Range within which objects can be picked up

    [Header("FRIDGE DOOR SETTINGS")]
    [Space(5)]
    public Transform Hinge;
    private bool Open;

    [Header("TAP SETTINGS")]
    [Space(5)]
    public ParticleSystem RunningWater;
    public GameObject WaterFlow;
    public ParticleSystem RunningWater1;
    public GameObject WaterFlow1;
    public ParticleSystem RunningWater2;
    public GameObject WaterFlow2;
    public ParticleSystem RunningWater3;
    public GameObject WaterFlow3;
    public ParticleSystem RunningWater4;
    public GameObject WaterFlow4;
    public ParticleSystem RunningWater5;
    public GameObject WaterFlow5;
    public ParticleSystem RunningWater6;
    public GameObject WaterFlow6;
    public bool TapOpen = false;
    public bool TapClosed = true;

    [Header("STOVE SETTINGS")]
    [Space(5)]
    public ParticleSystem StoveFlame;
    public GameObject GasFlame;
    public ParticleSystem StoveFlame1;
    public GameObject GasFlame1;
    public ParticleSystem StoveFlame2;
    public GameObject GasFlame2;
    public ParticleSystem StoveFlame3;
    public GameObject GasFlame3;
    public ParticleSystem StoveFlame4;
    public GameObject GasFlame4;
    public ParticleSystem StoveFlame5;
    public GameObject GasFlame5;
    public ParticleSystem StoveFlame6;
    public GameObject GasFlame6;
    public ParticleSystem StoveFlame7;
    public GameObject GasFlame7;
    public ParticleSystem StoveFlame8;
    public GameObject GasFlame8;
    public ParticleSystem StoveFlame9;
    public GameObject GasFlame9;
    public ParticleSystem StoveFlame10;
    public GameObject GasFlame10;
    public ParticleSystem StoveFlame11;
    public GameObject GasFlame11;
    public ParticleSystem StoveFlame12;
    public GameObject GasFlame12;
    public ParticleSystem StoveFlame13;
    public GameObject GasFlame13;
    public ParticleSystem StoveFlame14;
    public GameObject GasFlame14;
    public ParticleSystem StoveFlame15;
    public GameObject GasFlame15;
    public ParticleSystem StoveFlame16;
    public GameObject GasFlame16;
    public ParticleSystem StoveFlame17;
    public GameObject GasFlame17;
    public ParticleSystem StoveFlame18;
    public GameObject GasFlame18;
    public ParticleSystem StoveFlame19;
    public GameObject GasFlame19;
    public ParticleSystem StoveFlame20;
    public GameObject GasFlame20;
    public ParticleSystem StoveFlame21;
    public GameObject GasFlame21;
    public ParticleSystem StoveFlame22;
    public GameObject GasFlame22;
    public ParticleSystem StoveFlame23;
    public GameObject GasFlame23;
    public ParticleSystem StoveFlame24;
    public GameObject GasFlame24;
    public ParticleSystem StoveFlame25;
    public GameObject GasFlame25;
    public ParticleSystem StoveFlame26;
    public GameObject GasFlame26;
    public ParticleSystem StoveFlame27;
    public GameObject GasFlame27;
    public ParticleSystem StoveFlame28;
    public GameObject GasFlame28;
    public ParticleSystem StoveFlame29;
    public GameObject GasFlame29;
    public ParticleSystem StoveFlame30;
    public GameObject GasFlame30;
    public ParticleSystem StoveFlame31;
    public GameObject GasFlame31;
    public ParticleSystem StoveFlame32;
    public GameObject GasFlame32;
    public ParticleSystem StoveFlame33;
    public GameObject GasFlame33;
    public ParticleSystem StoveFlame34;
    public GameObject GasFlame34;
    
    public bool KnobOn = false;
    public bool KnobOff = true;

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
        CheckForPickUp(); // Check for pickup objects every frame


        if (Open && Hinge.rotation.z < 0.2f)
        {
            Hinge.Rotate(0, 0, -90 * Time.deltaTime);
        }
        else if (Hinge.rotation.z > 0.2f)
        {
            Open = false;
        }
        Debug.Log(Hinge.rotation.z);
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

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            hit.collider.CompareTag("Tap");

            if (hit.collider.CompareTag("Tap"))
            {
                if (TapOpen)
                {
                    TapOpen = false;
                    TapClosed = true;
                    WaterFlow.gameObject.SetActive(false);
                    RunningWater.Stop();
                }
                else
                {
                    TapOpen = true;
                    TapClosed = false;
                    WaterFlow.gameObject.SetActive(true);
                    RunningWater.Play();
                }
            }
        }

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            hit.collider.CompareTag("Tap1");

            if (hit.collider.CompareTag("Tap1"))
            {
                if (TapOpen)
                {
                    TapOpen = false;
                    TapClosed = true;
                    WaterFlow1.gameObject.SetActive(false);
                    RunningWater1.Stop();
                }
                else
                {
                    TapOpen = true;
                    TapClosed = false;
                    WaterFlow1.gameObject.SetActive(true);
                    RunningWater1.Play();
                }
            }
        }

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            hit.collider.CompareTag("Tap2");

            if (hit.collider.CompareTag("Tap2"))
            {
                if (TapOpen)
                {
                    TapOpen = false;
                    TapClosed = true;
                    WaterFlow2.gameObject.SetActive(false);
                    RunningWater2.Stop();
                }
                else
                {
                    TapOpen = true;
                    TapClosed = false;
                    WaterFlow2.gameObject.SetActive(true);
                    RunningWater2.Play();
                }
            }
        }

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            hit.collider.CompareTag("Tap3");

            if (hit.collider.CompareTag("Tap3"))
            {
                if (TapOpen)
                {
                    TapOpen = false;
                    TapClosed = true;
                    WaterFlow3.gameObject.SetActive(false);
                    RunningWater3.Stop();
                }
                else
                {
                    TapOpen = true;
                    TapClosed = false;
                    WaterFlow3.gameObject.SetActive(true);
                    RunningWater3.Play();
                }
            }
        }

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            hit.collider.CompareTag("Tap4");

            if (hit.collider.CompareTag("Tap4"))
            {
                if (TapOpen)
                {
                    TapOpen = false;
                    TapClosed = true;
                    WaterFlow4.gameObject.SetActive(false);
                    RunningWater4.Stop();
                }
                else
                {
                    TapOpen = true;
                    TapClosed = false;
                    WaterFlow4.gameObject.SetActive(true);
                    RunningWater4.Play();
                }
            }
        }

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            hit.collider.CompareTag("Tap5");

            if (hit.collider.CompareTag("Tap5"))
            {
                if (TapOpen)
                {
                    TapOpen = false;
                    TapClosed = true;
                    WaterFlow5.gameObject.SetActive(false);
                    RunningWater5.Stop();
                }
                else
                {
                    TapOpen = true;
                    TapClosed = false;
                    WaterFlow5.gameObject.SetActive(true);
                    RunningWater5.Play();
                }
            }
        }

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            hit.collider.CompareTag("Tap6");

            if (hit.collider.CompareTag("Tap6"))
            {
                if (TapOpen)
                {
                    TapOpen = false;
                    TapClosed = true;
                    WaterFlow6.gameObject.SetActive(false);
                    RunningWater6.Stop();
                }
                else
                {
                    TapOpen = true;
                    TapClosed = false;
                    WaterFlow6.gameObject.SetActive(true);
                    RunningWater6.Play();
                }
            }
        }

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            hit.collider.CompareTag("Knob");

            if (hit.collider.CompareTag("Knob"))
            {
                if (KnobOn)
                {
                    KnobOn = false;
                    KnobOff = true;
                    GasFlame.gameObject.SetActive(false);
                    GasFlame1.gameObject.SetActive(false);
                    GasFlame2.gameObject.SetActive(false);
                    GasFlame3.gameObject.SetActive(false);
                    GasFlame4.gameObject.SetActive(false);
                    StoveFlame.Stop();
                }
                else
                {
                    KnobOn = true;
                    KnobOff = false;
                    GasFlame.gameObject.SetActive(true);
                    GasFlame1.gameObject.SetActive(true);
                    GasFlame2 .gameObject.SetActive(true);
                    GasFlame3.gameObject.SetActive(true);
                    GasFlame4.gameObject.SetActive(true);
                    StoveFlame.Play();
                }
            }
        }

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            hit.collider.CompareTag("Knob1");

            if (hit.collider.CompareTag("Knob1"))
            {
                if (KnobOn)
                {
                    KnobOn = false;
                    KnobOff = true;
                    GasFlame5.gameObject.SetActive(false);
                    GasFlame6.gameObject.SetActive(false);
                    GasFlame7.gameObject.SetActive(false);
                    GasFlame8.gameObject.SetActive(false);
                    GasFlame9.gameObject.SetActive(false);
                    StoveFlame.Stop();
                }
                else
                {
                    KnobOn = true;
                    KnobOff = false;
                    GasFlame5.gameObject.SetActive(true);
                    GasFlame6.gameObject.SetActive(true);
                    GasFlame7.gameObject.SetActive(true);
                    GasFlame8.gameObject.SetActive(true);
                    GasFlame9.gameObject.SetActive(true);
                    StoveFlame.Play();
                }
            }
        }

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            hit.collider.CompareTag("Knob2");

            if (hit.collider.CompareTag("Knob2"))
            {
                if (KnobOn)
                {
                    KnobOn = false;
                    KnobOff = true;
                    GasFlame10.gameObject.SetActive(false);
                    GasFlame11.gameObject.SetActive(false);
                    GasFlame12.gameObject.SetActive(false);
                    GasFlame13.gameObject.SetActive(false);
                    GasFlame14.gameObject.SetActive(false);
                    StoveFlame.Stop();
                }
                else
                {
                    KnobOn = true;
                    KnobOff = false;
                    GasFlame10.gameObject.SetActive(true);
                    GasFlame11.gameObject.SetActive(true);
                    GasFlame12.gameObject.SetActive(true);
                    GasFlame13.gameObject.SetActive(true);
                    GasFlame14.gameObject.SetActive(true);
                    StoveFlame.Play();
                }
            }
        }

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            hit.collider.CompareTag("Knob3");

            if (hit.collider.CompareTag("Knob3"))
            {
                if (KnobOn)
                {
                    KnobOn = false;
                    KnobOff = true;
                    GasFlame15.gameObject.SetActive(false);
                    GasFlame16.gameObject.SetActive(false);
                    GasFlame17.gameObject.SetActive(false);
                    GasFlame18.gameObject.SetActive(false);
                    GasFlame19.gameObject.SetActive(false);
                    StoveFlame.Stop();
                }
                else
                {
                    KnobOn = true;
                    KnobOff = false;
                    GasFlame15.gameObject.SetActive(true);
                    GasFlame16.gameObject.SetActive(true);
                    GasFlame17.gameObject.SetActive(true);
                    GasFlame18.gameObject.SetActive(true);
                    GasFlame19.gameObject.SetActive(true);
                    StoveFlame.Play();
                }
            }
        }

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            hit.collider.CompareTag("KnobFour");

            if (hit.collider.CompareTag("KnobFour"))
            {
                if (KnobOn)
                {
                    KnobOn = false;
                    KnobOff = true;
                    GasFlame20.gameObject.SetActive(false);
                    GasFlame21.gameObject.SetActive(false);
                    GasFlame22.gameObject.SetActive(false);
                    GasFlame23.gameObject.SetActive(false);
                    GasFlame24.gameObject.SetActive(false);
                    StoveFlame.Stop();
                }
                else
                {
                    KnobOn = true;
                    KnobOff = false;
                    GasFlame20.gameObject.SetActive(true);
                    GasFlame21.gameObject.SetActive(true);
                    GasFlame22.gameObject.SetActive(true);
                    GasFlame23.gameObject.SetActive(true);
                    GasFlame24.gameObject.SetActive(true);
                    StoveFlame.Play();
                }
            }
        }

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            hit.collider.CompareTag("KnobFive");

            if (hit.collider.CompareTag("KnobFive"))
            {
                if (KnobOn)
                {
                    KnobOn = false;
                    KnobOff = true;
                    GasFlame25.gameObject.SetActive(false);
                    GasFlame26.gameObject.SetActive(false);
                    GasFlame27.gameObject.SetActive(false);
                    GasFlame28.gameObject.SetActive(false);
                    GasFlame29.gameObject.SetActive(false);
                    StoveFlame.Stop();
                }
                else
                {
                    KnobOn = true;
                    KnobOff = false;
                    GasFlame25.gameObject.SetActive(true);
                    GasFlame26.gameObject.SetActive(true);
                    GasFlame27.gameObject.SetActive(true);
                    GasFlame28.gameObject.SetActive(true);
                    GasFlame29.gameObject.SetActive(true);
                    StoveFlame.Play();
                }
            }
        }

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            hit.collider.CompareTag("Knobvi");

            if (hit.collider.CompareTag("Knobvi"))
            {
                if (KnobOn)
                {
                    KnobOn = false;
                    KnobOff = true;
                    GasFlame30.gameObject.SetActive(false);
                    GasFlame31.gameObject.SetActive(false);
                    GasFlame32.gameObject.SetActive(false);
                    GasFlame33.gameObject.SetActive(false);
                    GasFlame34.gameObject.SetActive(false);
                    StoveFlame.Stop();
                }
                else
                {
                    KnobOn = true;
                    KnobOff = false;
                    GasFlame30.gameObject.SetActive(true);
                    GasFlame31.gameObject.SetActive(true);
                    GasFlame32.gameObject.SetActive(true);
                    GasFlame33.gameObject.SetActive(true);
                    GasFlame34.gameObject.SetActive(true);
                    StoveFlame.Play();
                }
            }
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

    private void CheckForPickUp()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        // Perform raycast to detect objects
        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            // Check if the object has the "PickUp" tag
            if (hit.collider.CompareTag("PickUp"))
            {
                // Display the pick-up text
                pickUpText.gameObject.SetActive(true);
                pickUpText.text = hit.collider.gameObject.name;  //name is the name of the game object - rename it 
            }
            else
            {
                // Hide the pick-up text if not looking at a "PickUp" object
                pickUpText.gameObject.SetActive(false);
            }
        }
        else
        {
            // Hide the text if not looking at any object
            pickUpText.gameObject.SetActive(false);
        }
    }

}