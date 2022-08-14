using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int answeredQuestions;
    int correctQuestions;
    

    public int GetCorrectAnswers()
    {
        return correctQuestions;
    }

    public void IncrementCorrectAnswers()
    {
        correctQuestions++;
    }

    public int GetAnsweredQuestions()
    {
        return answeredQuestions;
    }

    public void IncrementAnsweredQuestions()
    {
        answeredQuestions++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctQuestions/(float)answeredQuestions*100);
    }


}



    
