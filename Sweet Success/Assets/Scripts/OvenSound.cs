using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenSound : MonoBehaviour
{
    AudioSource ovenAudio;
    public GameObject ovenSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OvenDone()
    {
        ovenAudio = ovenSource.GetComponent<AudioSource>();
        ovenAudio.Play();
    }
}
