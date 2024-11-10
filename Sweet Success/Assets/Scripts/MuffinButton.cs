using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuffinButton : MonoBehaviour
{
    public Transform tableSpawnPoint;

    public GameObject muffinBatterAndTray;

    public GameObject transferToTrayButtonB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TransferMuffin()
    {
        // Instantiate at the spawn point
        GameObject MuffinTray = Instantiate(muffinBatterAndTray, tableSpawnPoint.position, tableSpawnPoint.rotation);

        // Get the Rigidbody component and set its velocity
        Rigidbody rb = MuffinTray.GetComponent<Rigidbody>();
        rb.velocity = tableSpawnPoint.forward * 0f;

        transferToTrayButtonB.SetActive(false);
    }
}
