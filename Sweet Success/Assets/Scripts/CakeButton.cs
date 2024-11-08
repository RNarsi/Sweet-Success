using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeButton : MonoBehaviour
{
    public Transform tableSpawnPoint;

    public GameObject cakeBatterAndTray;

    public GameObject transferToTrayButtonCA;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TransferCake()
    {
        // Instantiate at the spawn point
        GameObject CakeTray = Instantiate(cakeBatterAndTray, tableSpawnPoint.position, tableSpawnPoint.rotation);

        // Get the Rigidbody component and set its velocity
        Rigidbody rb = CakeTray.GetComponent<Rigidbody>();
        rb.velocity = tableSpawnPoint.forward * 0f;

        transferToTrayButtonCA.SetActive(false);
    }
}
