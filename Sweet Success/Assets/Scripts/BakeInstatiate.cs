using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeInstatiate : MonoBehaviour
{
    
    public GameObject ovenLoad;
    public GameObject MuffinTray;
    public GameObject MuffinBake;
    public GameObject CookieTray;
    public GameObject CookieBake;
    public GameObject CakeTray;
    public GameObject CakeBake;

    public float delayTime = 8.0f;

    private void Start()
    {
        ovenLoad.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "MuffinTray")
        {

            StartCoroutine(InstantiateMuffinAfterDelay());

        }
        //if (other.gameObject.tag == "CookieTray")
        //{
        //    StartCoroutine(InstantiateAfterDelay());
        //}
        //if (other.gameObject.tag == "CakeTray")
        //{
        //    StartCoroutine(InstantiateAfterDelay());
        //}
        if (other.gameObject.tag == "CookieTray")
        {
            StartCoroutine(InstantiateCookieAfterDelay());

        }

        if (other.gameObject.tag == "CakeTray")
        {
            StartCoroutine(InstantiateCakeAfterDelay());
        }
    }

    

    private IEnumerator InstantiateMuffinAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        Instantiate(MuffinBake, MuffinTray.transform.position, MuffinTray.transform.rotation);
        Destroy(MuffinTray);
        MuffinTray = null;

    }

    private IEnumerator InstantiateCookieAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        Instantiate(CookieBake, CookieTray.transform.position, CookieTray.transform.rotation);
        Destroy(CookieTray);
        CookieTray = null;

    }

    private IEnumerator InstantiateCakeAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        Instantiate(CakeBake, CakeTray.transform.position, CakeTray.transform.rotation);
        Destroy(CakeTray);
        CakeTray = null;

    }
}
