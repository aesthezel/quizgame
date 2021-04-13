using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Example : MonoBehaviour
{
    [SerializeField] private string exampleName;
    public Text questionText;
    public Text resultText;
    public InputField inputAnswer;

    public string[] questions;
    public string[] answers;
    private int whatQuestionNumber;

    private bool[] questionsSolved;
    public int questionChecked;

    private void Start()
    {
        questionsSolved = new bool[questions.Length];
        GenerateQuestion();
    }

    private void GenerateQuestion()
    {
        do
        {
            whatQuestionNumber = Random.Range(0, questions.Length);
            QuestionIsShowed(whatQuestionNumber);
        } while (QuestionIsShowed(whatQuestionNumber) == true);

        if(questionChecked == questionsSolved.Length)
        {
            EndGame();
        }

        questionText.text = questions[whatQuestionNumber];
    }

    private void EndGame()
    {
        questionText.text = "Fin del juego";
        resultText.text = "Ganaste";
    }

    private bool QuestionIsShowed(int question)
    {
        for (int i = 0; i < questionsSolved.Length; i++)
        {
            if(questionsSolved[i] == true)
            {
                questionChecked++;
            }
        }

        if(questionChecked == questionsSolved.Length)
        {
            return true;
        }

        if(questionsSolved[question] == false)
        {
            questionsSolved[question] = true;
            return true;
        }

        return false;
    }

    public void ConfirmAnswer()
    {
        exampleName = inputAnswer.text;

        if(inputAnswer.text.ToLower() == answers[whatQuestionNumber].ToLower())
        {
            StartCoroutine("ShowResult", "Correcto");
            GenerateQuestion();
        }
        else
        {
            StartCoroutine("ShowResult", "Incorrecto");
        }
    }

    private IEnumerator ShowResult(string result)
    {
        resultText.text = result;
        yield return new WaitForSeconds(1.5f);
        resultText.text = "";
    }

}