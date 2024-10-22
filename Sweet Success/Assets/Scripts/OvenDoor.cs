using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class OvenDoor : MonoBehaviour
{
    [SerializeField] private Animator ovenDoor;

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.CompareTag("Player"))
          {
           bool isOpen = ovenDoor.GetBool("isOpen");

            if (isOpen)
            {
                ovenDoor.SetTrigger("Open");
                ovenDoor.SetBool("isOpen", true);
            }
            //else
            //{
            //ovenDoor.SetTrigger("Open");
            //ovenDoor.SetBool("isOpen", false);
            //}

        }
    }

    private void OnTriggerExit(Collider other)
    {
        ovenDoor.SetTrigger("Close");
        ovenDoor.SetBool("isOpen", false);

        
    }
}
            


        //[SerializeField] private bool openTrigger;


        //private void OnTriggerStay(Collider other)
        //{
        //    if (other.CompareTag("Trigger"))
        //    {
        //        ovenDoor.SetBool("openTrigger", true);
        //        StartCoroutine("StopDoor");
        //    }
        //    else
        //    {
        //        ovenDoor.SetBool("openTrigger", false);
        //    }

        //}
        //    public Transform Hinge;
        //    public float openAngle;
        //    private bool open;

        //    private void OnTriggerEnter(Collider other)
        //    {
        //        OpenDoor();
        //    }
        //    public void OnTriggerExit(Collider other)
        //    {
        //        CloseDoor();
        //    }

        //    public void OpenDoor()
        //    {
        //        Debug.Log("Opening");
        //        Hinge.Rotate(openAngle, 0, 0);
        //    }

        //    public void CloseDoor()
        //    {
        //        Debug.Log("closing");
        //        Hinge.Rotate(-openAngle, 0, 0); 
        //    }


        //IEnumerator StopDoor()
        //{
        //    yield return new WaitForSeconds(0.5f);
        //    ovenDoor.SetBool("openTrigger", false); 
        //    ovenDoor.enabled = false;   
        //}
//    }
//}
