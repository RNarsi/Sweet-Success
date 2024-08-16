using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "FridgeDoor")
        {
            Animator anim = other.GetComponentInChildren<Animator>(); //detect which has animator atttached 
            if(Input.GetKeyDown(KeyCode.O))
            {
                anim.SetTrigger("OpenClose(1)"); //Key O is meant for open
            }  
        }
    } 
    


    //Above is the code for open door mechanic. Our door animator works but it opens on start, without being trigger.
    

    //Iteration of open door mechanic:
/* [SerializedField] privte Animator myDoor = null;
 [SerializeField] private bool openTrigger = false;
 [SerializeField] private bool closeTrigger = false;

 private void OnTriggerEnter(Collider other)
 {
     if (other.CompareTag("Player"))
     {
         if (openTrigger)
         {
             myDoor.Play("Door Hinge(1)_Open", 0, 0.0f);
             gameObject.SetActive(false);
         }

         else if (closeTrigger)
         {
             myDoor.Play("Door Hinge(1)_Close", 0, 0.0f);
             gameObject.SetActive(false);
         }

     }
 } */

}
