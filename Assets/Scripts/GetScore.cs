using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetScore : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = "Score: " + GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<Timer>().ActualScore.ToString();
    }
}
