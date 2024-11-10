using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeInstantiate : MonoBehaviour
{
    //public GameObject ovenLoad;
    //public GameObject MuffinTray;
    //public GameObject MuffinBake;
    //public GameObject CookieTray;
    //public GameObject CookieBake;
    //public GameObject CakeTray;
    //public GameObject CakeBake;

    public GameObject done1Button;
    public GameObject done2Button;
    public GameObject done3Button;

    public GameObject ovenLoad1Button;
    public GameObject ovenLoad2Button;
    public GameObject ovenLoad3Button;

    //[Header("OVEN BAKE")]
    //[Space(5)]
    //public GameObject MuffinBake;
    //public GameObject MuffinTray;
    //public GameObject CakeBake;
    //public GameObject CakeTray;
    //public GameObject CookieTray;
    //public GameObject CookieBake;

    public float delayTime = 8.0f;

    private void Start()
    {
        //ovenLoad.SetActive(false);

        done1Button.SetActive(false);
        done2Button.SetActive(false);
        done3Button.SetActive(false);

        ovenLoad1Button.SetActive(false);
        ovenLoad2Button.SetActive(false);
        ovenLoad3Button.SetActive(false);

        //MuffinTray.SetActive(false);
        //CookieTray.SetActive(false);
        //CakeTray.SetActive(false);
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MuffinTray"))
        {
            ovenLoad1Button.SetActive(true);
            done1Button.SetActive(true );
            
        }

        if (other.CompareTag("CookieTray"))
        {
            ovenLoad2Button.SetActive(true);
            done2Button.SetActive(true);
        }

        if (other.CompareTag("CakeTray"))
        {
            ovenLoad3Button.SetActive(true);
            done3Button.SetActive (true);
        }
    }

    //public void BakeMuffin()
    //{
    //    MuffinTray.SetActive(false);
    //    MuffinBake.SetActive(true);
    //}

    //public void BakeCookie()
    //{
    //    CookieTray.SetActive(false);
    //    CookieBake.SetActive(true);
    //}

    //public void BakeCake()
    //{
    //    CakeTray.SetActive(false);
    //    CakeBake.SetActive(true);
    //}
}


