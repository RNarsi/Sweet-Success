using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Image timer_linear_image;
    public Image timer_radial_image;
    public GameObject restart_game_textholder;
    public GameObject restartMenu;
    public float time_remaining;
    public float max_time = 900.0f;
    public GameObject timer_radial_textholder;


    void Start()
    {
        time_remaining = max_time;
    }

    // Update is called once per frame
    void Update()
    {
        //check whether time remaining is not equal to zero 
        if (time_remaining > 0)
        {
            time_remaining -= Time.deltaTime;    //real time seconds 
            timer_linear_image.fillAmount = time_remaining / max_time;
            timer_radial_image.fillAmount -= time_remaining / max_time; 

        }
        else    //time is 0 and we want to display the text 
        {
            restartMenu.SetActive(true);
            timer_radial_textholder.SetActive(false);

        }
        //add if statement for the bake function, the radial time to be set active or only work when something is in the oven

    }
}
