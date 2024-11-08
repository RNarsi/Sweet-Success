using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;
    //public Button doneButton;
    int score = 0;
    public GameObject endGame;


    private void Awake()
    {
        instance = this;

    }

    void Start()
    {
        endGame.SetActive(false);
        score = 0;
        scoreText.text = "Recipe " + score.ToString();
    }

    public void AddPoint()
    {
        score += 1;
        scoreText.text = "Recipe " + score.ToString();
        End();
    }

    public void End()
    {
        if (score >= 2)
        {
            Debug.Log("game finished");
            endGame.SetActive(true);
        }
    }

}