using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlScript : MonoBehaviour
{
    public float bowlRange = 2f;
    public Transform bowlTransform;
    public Transform bowlTransform1;
    public Transform bowlTransform2;
    public Transform bowlTransform3;
    public Transform bowlTransform4;
    public GameObject transferToTrayButtonB;
    public GameObject transferToTrayButtonCC;
    public GameObject transferToTrayButtonCA;
    public GameObject Bowl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = new Ray(bowlTransform.position, bowlTransform.forward);
        RaycastHit hit;

        Debug.DrawRay(bowlTransform.position, bowlTransform.forward * bowlRange, Color.red, 2f);

        Ray ray1 = new Ray(bowlTransform1.position, bowlTransform1.forward);
        RaycastHit hit1;

        Debug.DrawRay(bowlTransform1.position, bowlTransform1.forward * bowlRange, Color.red, 2f);

        Ray ray2 = new Ray(bowlTransform2.position, bowlTransform2.forward);
        RaycastHit hit2;

        Debug.DrawRay(bowlTransform2.position, bowlTransform2.forward * bowlRange, Color.red, 2f);

        Ray ray3 = new Ray(bowlTransform3.position, bowlTransform3.forward);
        RaycastHit hit3;

        Debug.DrawRay(bowlTransform3.position, bowlTransform3.forward * bowlRange, Color.red, 2f);

        Ray ray4 = new Ray(bowlTransform4.position, bowlTransform4.forward);
        RaycastHit hit4;

        Debug.DrawRay(bowlTransform4.position, bowlTransform4.forward * bowlRange, Color.red, 2f);


        if (Physics.Raycast(ray, out hit, bowlRange))
        {
            if (hit.collider.CompareTag("blueberryPrefab"))
            {
                transferToTrayButtonB.SetActive(true);
            }
        }
        if (Physics.Raycast(ray, out hit, bowlRange))
        {
            if (hit.collider.CompareTag("chocChipPrefab"))
            {
                transferToTrayButtonCC.SetActive(true);
            }
        }
        if (Physics.Raycast(ray, out hit, bowlRange))
        {
            if (hit.collider.CompareTag("chocChipPrefab"))
            {
                transferToTrayButtonCC.SetActive(true);
            }
        }


        if (Physics.Raycast(ray1, out hit1, bowlRange))
        {
            if (hit1.collider.CompareTag("blueberryPrefab"))
            {
                transferToTrayButtonB.SetActive(true);
            }
        }
        if (Physics.Raycast(ray1, out hit1, bowlRange))
        {
            if (hit1.collider.CompareTag("chocChipPrefab"))
            {
                transferToTrayButtonCC.SetActive(true);
            }
        }
        if (Physics.Raycast(ray1, out hit1, bowlRange))
        {
            if (hit1.collider.CompareTag("chocChipPrefab"))
            {
                transferToTrayButtonCC.SetActive(true);
            }
        }


        if (Physics.Raycast(ray2, out hit2, bowlRange))
        {
            if (hit2.collider.CompareTag("blueberryPrefab"))
            {
                transferToTrayButtonB.SetActive(true);
            }
        }
        if (Physics.Raycast(ray2, out hit2, bowlRange))
        {
            if (hit2.collider.CompareTag("chocChipPrefab"))
            {
                transferToTrayButtonCC.SetActive(true);
            }
        }
        if (Physics.Raycast(ray2, out hit2, bowlRange))
        {
            if (hit2.collider.CompareTag("blueberryPrefab"))
            {
                transferToTrayButtonB.SetActive(true);
            }
        }


        if (Physics.Raycast(ray3, out hit3, bowlRange))
        {
            if (hit3.collider.CompareTag("blueberryPrefab"))
            {
                transferToTrayButtonB.SetActive(true);
            }
        }
        if (Physics.Raycast(ray3, out hit3, bowlRange))
        {
            if (hit3.collider.CompareTag("chocChipPrefab"))
            {
                transferToTrayButtonCC.SetActive(true);
            }
        }
        if (Physics.Raycast(ray3, out hit3, bowlRange))
        {
            if (hit3.collider.CompareTag("blueberryPrefab"))
            {
                transferToTrayButtonB.SetActive(true);
            }
        }


        if (Physics.Raycast(ray4, out hit4, bowlRange))
        {
            if (hit4.collider.CompareTag("blueberryPrefab"))
            {
                transferToTrayButtonB.SetActive(true);
            }
        }
        if (Physics.Raycast(ray4, out hit4, bowlRange))
        {
            if (hit4.collider.CompareTag("chocChipPrefab"))
            {
                transferToTrayButtonCC.SetActive(true);
            }
        }
        if (Physics.Raycast(ray4, out hit4, bowlRange))
        {
            if (hit4.collider.CompareTag("blueberryPrefab"))
            {
                transferToTrayButtonB.SetActive(true);
            }
        }
    }

    //public void LoadTransferButton()
    //{
    //    Ray ray = new Ray(bowlTransform.position, bowlTransform.forward);
    //    RaycastHit hit;

    //    Debug.DrawRay(bowlTransform.position, bowlTransform.forward * bowlRange, Color.red, 2f);

    //    if (Physics.Raycast(ray, out hit, bowlRange))
    //    {
    //        hit.collider.CompareTag("blueberryPrefab");

    //        transferToTrayButton.SetActive(true);
    //    }
    //    else
    //    {

    //    }
    //}

}
