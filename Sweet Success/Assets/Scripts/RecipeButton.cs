using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeButton : MonoBehaviour
{
    public Transform playerCamera;
    public GameObject recipeButton1;
    public GameObject recipeButton2;
    public GameObject recipeButton3;
    public float pickUpRange = 3f;
   

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
            }
            else
            {
                recipeButton1.SetActive(false);
            }

            if (hit.collider.CompareTag("Recipe2"))
            {
                recipeButton2.SetActive(true);
            }
            else
            {
                recipeButton2.SetActive(false);
            }

            if (hit.collider.CompareTag("Recipe3"))
            {
                recipeButton3.SetActive(true);
            }
            else
            {
                recipeButton3.SetActive(false);
            }

        }
        else
        {
            recipeButton1.SetActive(false);
            recipeButton2.SetActive(false);
            recipeButton3.SetActive(false);
        }

    }
}
