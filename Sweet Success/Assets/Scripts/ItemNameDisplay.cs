using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
/*
public class ItemNameDisplay : MonoBehaviour
{

    [Header("UI Settings")]
    [Space(5)]
    public TextMeshProUGUI guideText;

    public GameObject playerCamera;
    public FirstPersonControls firstPersonControls;
    public float pickUpRange = 3f;
    public LayerMask Interact;
    public LayerMask BigEquipment;
    public LayerMask SmallEquipment;
    public LayerMask Ingredients;

    private void Update()
    {
        CheckForPickup();
    }
    public void CheckForPickup()
    {
        Ray ray = new Ray(firstPersonControls.playerCamera.position, firstPersonControls.playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickUpRange, Interact))
            {
            GameObject Object = hit.collider.gameObject;
            guideText.text = hit.collider.name + " - Click 'F' To Interact";
        }
        else if (Physics.Raycast(ray, out hit, pickUpRange, Ingredients))
        {
            GameObject Object = hit.collider.gameObject;
            guideText.text = hit.collider.name + "Click 'P' To PickUp";
        }

        if (Physics.Raycast(ray, out hit, pickUpRange, SmallEquipment))
        {
            GameObject Object = hit.collider.gameObject;
            guideText.text = hit.collider.name + " - Click 'E' To Interact";
        }
        else if (Physics.Raycast(ray, out hit, pickUpRange, BigEquipment))
        {
            GameObject Object = hit.collider.gameObject;
            guideText.text = hit.collider.name + "Click 'E' To PickUp";
        }
    }
    
}
*/