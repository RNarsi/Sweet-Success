using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Camera mainCamera; // Reference to the camera (assign in the Inspector)
    public float rotationSpeed = 1f; // Speed at which the camera rotates
    private bool isRotating = false; // Track if the camera is currently rotating
    public GameObject[] UIElements;
    private Controls playerInput;
    public GameObject initialButton;
    public GameObject pausePage;

    [Header("RECIPE BOOK")]
    [Space(5)]
    public GameObject RecipeBook1;
    public GameObject RecipeBook2;
    public GameObject RecipeBook3;

    [Header("RECIPE BUTTON")]
    [Space(5)]
    public GameObject recipeButton1;
    public GameObject recipeButton2;
    public GameObject recipeButton3;

    [Header("OVEN BAKE")]
    [Space(5)]
    public GameObject MuffinBake;
    public GameObject MuffinTray;
    public GameObject CakeBake;
    public GameObject CakeTray;
    public GameObject CookieTray;
    public GameObject CookieBake;
    public GameObject ovenLoad1;
    public GameObject ovenLoad2;
    public GameObject ovenLoad3;

    public GameObject doneButton1;
    public GameObject doneButton2;
    public GameObject doneButton3;

    //public GameObject RecipeBook;

    //private Controls Georgie; 

    //public GameObject resumePage;
    //public GameObject quitPage;

    public GameObject muffinBatterAndTray;
    //public GameObject tableStation;
    public Transform tableSpawnPoint;
    public GameObject transferToTrayButtonB;

    //public GameObject cookieDoughAndTray;

    bool GamePaused;

    // Method to rotate the camera left by 90 degrees
    public void RotateCameraLeftBy90Degrees()
    {
        if (!isRotating) // Prevent triggering multiple rotations simultaneously
        {
            StartCoroutine(RotateCameraCoroutine(90f));
        }
    }

    private void Start()
    {
        GamePaused = false;

        //recipeButton1.SetActive(true);
        //recipeButton2.SetActive(true);
        //recipeButton3.SetActive(true);
    }

    private void Update()
    {
        if (GamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
       
    }

    // Coroutine to smoothly rotate the camera
    private IEnumerator RotateCameraCoroutine(float angle)
    {
        isRotating = true;

        Quaternion startRotation = mainCamera.transform.rotation; // Initial rotation
        Quaternion endRotation = startRotation * Quaternion.Euler(0, -angle, 0); // Target rotation

        float rotationProgress = 0f;
        while (rotationProgress < 1f)
        {
            rotationProgress += Time.deltaTime * (rotationSpeed / angle); // Normalize the rotation speed based on angle
            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, endRotation, rotationProgress); // Smoothly interpolate rotation
            yield return null;
        }

        mainCamera.transform.rotation = endRotation; // Ensure exact final rotation
        isRotating = false;

        initialButton.SetActive(false);

        foreach (GameObject UIelement in UIElements)
        {
            UIelement.SetActive(true);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Pause()
    {
        GamePaused = true;
        pausePage.SetActive(true);
        // playerInput.Disable();
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    //public void TransferMuffin()
    //{
    //    // Instantiate at the spawn point
    //    GameObject MuffinTray = Instantiate(muffinBatterAndTray, tableSpawnPoint.position, tableSpawnPoint.rotation);

    //    // Get the Rigidbody component and set its velocity
    //    Rigidbody rb = MuffinTray.GetComponent<Rigidbody>();
    //    rb.velocity = tableSpawnPoint.forward * 0f;

    //    transferToTrayButtonB.SetActive(false);
    //}

    //public void TransferCookie()
    //{
    //    // Instantiate at the spawn point
    //    GameObject CookieTray = Instantiate(cookieDoughAndTray, tableSpawnPoint.position, tableSpawnPoint.rotation);

    //    // Get the Rigidbody component and set its velocity
    //    Rigidbody rb = CookieTray.GetComponent<Rigidbody>();
    //    rb.velocity = tableSpawnPoint.forward * 0f;
    //}

    public void DoneRecipe()
    {
        ScoreManager.instance.AddPoint();
    }

    public void BakeMuffin()
    {
        MuffinTray.SetActive(false);
        MuffinBake.SetActive(true);
        ovenLoad1.SetActive(false);
    }

    public void BakeCookie()
    {
        CookieTray.SetActive(false);
        CookieBake.SetActive(true);
        ovenLoad2.SetActive(false);
    }

    public void BakeCake()
    {
        CakeTray.SetActive(false);
        CakeBake.SetActive(true);
        ovenLoad3.SetActive(false); 
    }


    public void OnClickMuffin()
    {
        doneButton1.SetActive(false);
    }

    public void OnClickCake()
    {
        doneButton3.SetActive(false);
    }

    public void OnClickCookie()
    {
        doneButton2.SetActive(false);
    }

    public void ViewRecipe1()
    {
        recipeButton1.SetActive(false);
    }

    public void ViewRecipe2()
    {
        recipeButton2.SetActive(false);
    }

    public void ViewRecipe3()
    {
        recipeButton3.SetActive(false);
    }
}
