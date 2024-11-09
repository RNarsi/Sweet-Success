using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeInstatiate : MonoBehaviour
{

    public GameObject MuffinTray;
    public GameObject MuffinBake;
    public GameObject CookieTray;
    public GameObject CookieBake;
    public GameObject CakeTray;
    public GameObject CakeBake;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "MuffinTray")
        {
            Instantiate(MuffinBake, MuffinTray.transform.position, MuffinTray.transform.rotation);
            Destroy(MuffinTray);
        }

        if (other.gameObject.tag == "CookieTray")
        {
            Instantiate(CookieBake, CookieTray.transform.position, CookieTray.transform.rotation);
            Destroy(CookieTray);
        }

        if (other.gameObject.tag == "CakeTray")
        {
            Instantiate(CakeBake, CakeTray.transform.position, CakeTray.transform.rotation);
            Destroy(CakeTray);
        }
    }
}
