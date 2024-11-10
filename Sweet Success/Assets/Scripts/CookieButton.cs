using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieButton : MonoBehaviour
{
    //public GameObject tableStation;
    public Transform tableSpawnPoint;

    public GameObject cookieDoughAndTray;

    public GameObject transferToTrayButtonCC;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TransferCookie()
    {
        cookieDoughAndTray.SetActive(true);
        //// Instantiate at the spawn point
        //GameObject CookieTray = Instantiate(cookieDoughAndTray, tableSpawnPoint.position, tableSpawnPoint.rotation);

        //// Get the Rigidbody component and set its velocity
        //Rigidbody rb = CookieTray.GetComponent<Rigidbody>();
        //rb.velocity = tableSpawnPoint.forward * 0f;
        //Debug.Log("has sapwned");

        transferToTrayButtonCC.SetActive(false);
    }
}
