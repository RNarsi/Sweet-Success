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
            if(Input.GetKeyDown(KeyCode.O) )
            anim.SetTrigger("OpenClose(1)");
        }
    }
}
