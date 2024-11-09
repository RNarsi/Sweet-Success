using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;

public class OvenDoor : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;


    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    [SerializeField] private string doorOpen = "Oven_Open";
    [SerializeField] private string doorClose = "Oven_Close";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                myDoor.Play(doorOpen, 0, 0.0f);
                //gameObject.SetActive(false);    
            }

            else if (closeTrigger)
            {
                myDoor.Play(doorClose, 0, 0.0f);
                //gameObject.SetActive(false);
            }
        }
    }
}
