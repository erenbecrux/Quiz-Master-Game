using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI questionText;
    QuestionSO currentQuestion;
    [SerializeField] List<QuestionSO> questions;
    [SerializeField] GameObject[] answerButtons;
    bool isAnsweredEarly = true;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Image timerImage;
    Timer timer;
    ScoreKeeper scorekeeper;
    [SerializeField] Slider progressBar;
    public bool isComplete=false;

    
    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scorekeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;


    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction; 
        if(timer.loadNextQuestion)
        {
            if(progressBar.value==progressBar.maxValue)
            {
                isComplete=true;
                return;
            }
            DisplayNewQuestion();
            timer.loadNextQuestion = false;
            isAnsweredEarly = false;
        }
        else if(isAnsweredEarly==false && timer.isAnswering==false)
        {
            DisplayAnswer(-1);
            SetButtonState(false);

        }
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        
        for(int i = 0; i<answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void DisplayNewQuestion()
    {
        if(questions.Count!=0)
        {
            
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            scorekeeper.IncrementAnsweredQuestions();
            progressBar.value++;
            DisplayQuestion();
        }
        
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0,questions.Count);
        currentQuestion = questions[index];
        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
        
    }

    void DisplayAnswer(int index)
    {
        
        if(index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "CORRECT!";
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scorekeeper.IncrementCorrectAnswers();
            

        }

        else
        {
            questionText.text = "Correct answer was\n "+ currentQuestion.GetAnswer(currentQuestion.GetCorrectAnswerIndex());
            Image buttonImage = answerButtons[currentQuestion.GetCorrectAnswerIndex()].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreText.text = "Score " + scorekeeper.CalculateScore() + " %";
            

        }
    }
    public void OnAnswerSelected(int index)
    {
        isAnsweredEarly =true;
        DisplayAnswer(index);
        timer.CancelTimer();
        SetButtonState(false);
        scoreText.text = "Score " + scorekeeper.CalculateScore() + " %";

        

    }

    void SetButtonState(bool state)
    {
        for(int i=0; i< answerButtons.Length;i++)
        {
            Button buttonComponent = answerButtons[i].GetComponent<Button>();
            buttonComponent.interactable = state;
        }
    }    

    void SetDefaultButtonSprites()
    {
        for(int i=0; i<answerButtons.Length;i++)
        {
            Image newButtonImage = answerButtons[i].GetComponent<Image>();
            newButtonImage.sprite = defaultAnswerSprite;
        }
    }

}
