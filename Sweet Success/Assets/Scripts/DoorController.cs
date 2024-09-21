using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoors : MonoBehaviour
{
    private bool Open;

    private void Update()
    {
        if (Open)
        {
            transform.Rotate(0, -170, 0);
        }
    }

}
