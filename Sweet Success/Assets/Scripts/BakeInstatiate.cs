using JetBrains.Annotations;
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

    public GameObject done1Button;
    public GameObject done2Button;
    public GameObject done3Button;

    public float delayTime = 8.0f;

    private void Start()
    {
        ovenLoad.gameObject.SetActive(false);

        done1Button.gameObject.SetActive(false);
        done2Button.gameObject.SetActive(false);
        done3Button.gameObject.SetActive(false);
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

        done1Button.SetActive(true);

    }

    private IEnumerator InstantiateCookieAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        Instantiate(CookieBake, CookieTray.transform.position, CookieTray.transform.rotation);
        Destroy(CookieTray);
        CookieTray = null;

        done2Button.SetActive(true);
    }

    private IEnumerator InstantiateCakeAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        Instantiate(CakeBake, CakeTray.transform.position, CakeTray.transform.rotation);
        Destroy(CakeTray);
        CakeTray = null;

        done3Button.SetActive(true);
    }
}
