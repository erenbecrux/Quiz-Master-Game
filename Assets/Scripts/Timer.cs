using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField] float timeAnswerQuestion;
    [SerializeField] float timeShowAnswer;

    public bool loadNextQuestion;
    public bool isAnswering;
    public float fillFraction;

    float timerValue;
    
    void Update()
    {
        UpdateTimer();
    }


    public void CancelTimer()
    {
        timerValue = 0;
        
    }

    void UpdateTimer()
    {
        if(isAnswering)
        {

            timerValue -= Time.deltaTime;
            if(timerValue>0)
            {
                fillFraction = timerValue/timeAnswerQuestion;

            }
            else if(timerValue<=0)
            {
                isAnswering = false;
                timerValue = timeShowAnswer;
                
            }
        }
        else if(isAnswering==false)
        {
            
            timerValue -= Time.deltaTime;
            if(timerValue>0)
            {
                fillFraction = timerValue/timeShowAnswer;
            }
            else if(timerValue<=0)
            {
                isAnswering = true;
                timerValue = timeAnswerQuestion;
                loadNextQuestion = true;
            }
        }

    
        
    }

}
