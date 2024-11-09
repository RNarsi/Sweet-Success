using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class RecipeButton : MonoBehaviour
{
    public Transform playerCamera;
    public GameObject recipeButton1;
    public GameObject recipeButton2;
    public GameObject recipeButton3;
    public float pickUpRange = 3f;

    private FirstPersonControls firstPersonControls;
    public GameObject Player;

    private void Awake()
    {
        firstPersonControls = Player.GetComponent<FirstPersonControls>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForRecipe();
    }

    public void CheckForRecipe()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        // Perform raycast to detect objects
        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            // Check if the object has the different interactables tags  
            if (hit.collider.CompareTag("Recipe1"))
            {
                recipeButton1.SetActive(true);
                Disable();
            }
            else
            {
                recipeButton1.SetActive(false);
                Enable();
            }

            if (hit.collider.CompareTag("Recipe2"))
            {
                recipeButton2.SetActive(true);
                Disable();
            }
            else
            {
                recipeButton2.SetActive(false);
                Enable();
            }

            if (hit.collider.CompareTag("Recipe3"))
            {
                recipeButton3.SetActive(true);
                Disable();
            }
            else
            {
                recipeButton3.SetActive(false);
                Enable();
            }

        }
        else
        {
            recipeButton1.SetActive(false);
            recipeButton2.SetActive(false);
            recipeButton3.SetActive(false);
        }


    }

    private void Disable()
    {
        if (firstPersonControls != null)
        {
            firstPersonControls.enabled = false;
        }
    }

    private void Enable()
    {
        if (firstPersonControls != null)
        {
            firstPersonControls.enabled = true;
        }
    }
}
