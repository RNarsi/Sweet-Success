using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OvenTimeController : MonoBehaviour
{
    public Image timer_linear_image;
    public GameObject timer_linear;
    //public Image timer_radial_image;
    //public GameObject restart_game_textholder;
    //public GameObject restartMenu;
    public float time_remaining;
    public float max_time = 5.0f;
    //public GameObject timer_radial_textholder;

    //public Transform ovenRack;
    //public float pickUpRange = 1f;
    //public GameObject replacementPrefab;



    void Start()
    {
        time_remaining = max_time;

    }


    // Update is called once per frame
    public void Update()
    {
        //timer_linear.SetActive(true);

    }

    public void BakingTimer()
    {
        if (time_remaining > 0)
        {
            time_remaining -= Time.deltaTime;    //real time seconds 
            timer_linear_image.fillAmount = time_remaining / max_time;

        }
        else    //time is 0 and we want to display the text 
        {
            timer_linear.SetActive(false);
            //timer_radial_textholder.SetActive(false);

        }
    }

}

