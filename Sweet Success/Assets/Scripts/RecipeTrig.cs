using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeTrig : MonoBehaviour
{

    public GameObject RecipeBook2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RecipeBook2.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            RecipeBook2.SetActive(false);
        }
    }

}
