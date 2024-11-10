using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeTriggers : MonoBehaviour
{

    public GameObject RecipeBook1;
    private void OnTriggerEnter(Collider other)
    {
       if (other.tag == "Player")
       {
            RecipeBook1.SetActive(true);
       }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            RecipeBook1.SetActive(false);
        }
    }

}
