using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI endText;
    ScoreKeeper scorekeeper;

    void Awake()
    {
        scorekeeper = FindObjectOfType<ScoreKeeper>();
        
    }

    public void ShowFinalScore()
    {
        endText.text = "Congratulations!\n You Scored " + scorekeeper.CalculateScore() + " %";
    }

  
}
