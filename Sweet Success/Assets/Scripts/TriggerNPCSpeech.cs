using UnityEngine;
using UnityEngine.UI; // Include this if you're using the standard UI
// using TMPro; // Uncomment this if you're using TextMeshPro

public class TriggerPanelController : MonoBehaviour
{
    public GameObject panel; // Reference to the UI panel
    public float displayTime = 8f; // Time in seconds to display the panel

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC")) // Check if the object has the "Player" tag
        {
            ShowPanel();
        }
    }

    private void ShowPanel()
    {
        panel.SetActive(true); // Activate the panel
        Invoke("HidePanel", displayTime); // Schedule to hide the panel after a few seconds
    }

    private void HidePanel()
    {
        panel.SetActive(false); // Deactivate the panel
    }
}
