//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class OvenTimeController : MonoBehaviour
//{
//    public  Image timer_radial_image;
//    public GameObject restart_game_textholder;
//    public GameObject doneMenu;
//    public float time_remaining;
//    public float max_time = 900.0f;
//    public GameObject timer_radial_textholder; 


//    void Start()
//    {
//        time_remaining = max_time;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if  (time_remaining > 0)
//        {
//            time_remaining -= Time.deltaTime;
//            timer_radial_image.fillAmount = time_remaining / max_time;
//        }
//        else
//        {
//            timer_radial_textholder.SetActive(false);
//        }
//    }
//}
