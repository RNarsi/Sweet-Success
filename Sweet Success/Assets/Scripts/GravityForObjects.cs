using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityForObjects : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
