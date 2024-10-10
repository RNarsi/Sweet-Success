using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform Hinge;
    public float openAngle;
    private bool open;

    private void OnTriggerEnter(Collider other)
    {
        OpenDoor();
    }
    public void OnTriggerExit(Collider other)
    {
        CloseDoor();
    }

    public void OpenDoor()
    {
        Debug.Log("Opening");
        Hinge.Rotate(0, openAngle, 0);
    }

    public void CloseDoor()
    {
        Debug.Log("closing");
        Hinge.Rotate(0, -openAngle, 0);
    }

}
