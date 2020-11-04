using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Timer : MonoBehaviour
{
    public GameObject scoreObject;
    float timer = 0.0f;
    TextMeshProUGUI Text;
    public int ActualScore = 0;
    public int HealthRemaining = 0;
    public int CamPosition = 0;
    // Start is called before the first frame update
    void Awake()
    {
        var scores = GameObject.FindGameObjectsWithTag("ScoreManager");
        if(scores.Length > 1)
        {
            Destroy(scores[0]);
            timer = 0f;
            ActualScore = 0;
        }
       
        Text = scoreObject.GetComponent<TextMeshProUGUI>();
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainScene" || SceneManager.GetActiveScene().name == "Level2")
        {
            timer += Time.deltaTime;
            string output = String.Format("Time: {0:0}", timer);
            //Debug.Log(timer);
            if (output != Text.text)
            {
                Text.text = output;
            }
        }
        else
        {
            timer = 0f;
            HealthRemaining = 0;
        }
        
    }

    public void CalculateScore()
    {
        var timeScore = Mathf.Max(1, 120 - timer);
        if(HealthRemaining > 0)
        {
            ActualScore = Mathf.RoundToInt((timeScore * Mathf.Max(HealthRemaining, 1)) + CamPosition);
        }
        else
        {
            ActualScore = CamPosition;
        }
        
    }
}
