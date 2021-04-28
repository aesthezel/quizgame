using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public enum QuizStates_FernandoPI
{
Resolving,
Solved,
End 
}



public class Quiz_FernandoPI : MonoBehaviour
{
    public QuizStates_FernandoPI quizState;
    public TMP_Text questionText;
    public TMP_Text resultText;
    public TMP_InputField answerField;

    public string[] questions;
    public string[] answers;
    private bool[] solvedQuestions;
    private int randomQuestionNumber;

    public Image lifeBar;

    public float actualHealth;
    public float maxHealth;
    public TMP_Text lossText;

    public QuizStates_FernandoPI GetRandomNumber()
    {
         randomQuestionNumber = Random.Range(0, questions.Length);

        if(solvedQuestions[randomQuestionNumber]== false)
        {
            solvedQuestions[randomQuestionNumber]= true;
            return QuizStates_FernandoPI.Resolving;
        }
        int countQuestion = 0;
        for (int i= 0; i < questions.Length; i++)
        {
            if(solvedQuestions[i]==true)
            {
                countQuestion++;
            }
        } 

        if(countQuestion== solvedQuestions.Length){
            
            return QuizStates_FernandoPI.End;

        }
        return QuizStates_FernandoPI.Solved;
    }

    public void GenerateQuestion()
    {
       quizState = GetRandomNumber();

        switch(quizState)
        {
            case QuizStates_FernandoPI.Resolving:
                questionText.text = questions[randomQuestionNumber];
                break;

            case QuizStates_FernandoPI.Solved:
                GenerateQuestion();
                break;

            case QuizStates_FernandoPI.End:
                EndGame();
                break;
        }
    }

    private void EndGame(){
        questionText.text = "No hay mas preguntas";
        resultText.text = "se acabo el juego";
        answerField.interactable = false;

    }

    public void ConfirmAnswer()
    {
        if(answerField.text == answers[randomQuestionNumber])
        {
            resultText.text = "Correcto";
            GenerateQuestion();
        }
        else
        {
            resultText.text = "Incorrecto";
            actualHealth -= 16.666f;
        }
    }
    void Start()
    {
        solvedQuestions = new bool[questions.Length];
        GenerateQuestion();
    }

    void Update()
    {
        lifeBar.fillAmount = actualHealth / maxHealth;
        gameOver();
    }

    private void gameOver()
    {
        if(actualHealth <= 0)
        {
            questionText.text = "Haz perdido, intenta de nuevo c;";
            answerField.interactable = false;
            if(Input.GetMouseButtonDown(0))
            {
                Restart();
            }
            resultText.text = "Haz click para reiniciar";

        }
    }

    private void Restart()
    {
        SceneManager.LoadScene("Level_FernandoPI");
    }
}
