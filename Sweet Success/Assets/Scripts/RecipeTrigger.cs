using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeTrigger : MonoBehaviour
{

    public GameObject RecipeBook3;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RecipeBook3.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            RecipeBook3.SetActive(false);
        }
    }

}
