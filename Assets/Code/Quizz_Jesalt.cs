using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public enum Status
{
    Resolve,
    solved,
    end
}
public class Quizz_Jesalt : MonoBehaviour
{

    public Status Quizz_Status;

    public TMP_Text QuestionText;
    public TMP_Text ResultText;
    public TMP_InputField TextField;

    public GameObject Cuadro;


    public string[] Question;
    public string[] Answer;
    private bool[] solveQuestion;

    private int Ran = 0;
    public void Event()
    {
        

        if (TextField.text.Equals(Answer[Ran]))
        {
            ResultText.text = ("Correcto");
            Cuadro.GetComponent<SpriteRenderer>().color = Color.green;
            //ResultText.color = Color.green;
            StartCoroutine("Wait");
            generateQuestion();
        }
        else
        {
            ResultText.text = ("Incorrecto");
            StartCoroutine("Wait");
            Cuadro.GetComponent<SpriteRenderer>().color = Color.red;
            //ResultText.color = Color.red;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        solveQuestion = new bool[Question.Length];
        generateQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private Status RandomNumber()
    {
        Ran = Random.Range(0, Question.Length);
        if (solveQuestion[Ran]==false)
        {
            solveQuestion[Ran] = true;
            return Status.Resolve;
          
        }

        int cont = 0;
        for(int i=0; i<Question.Length; i++)
        {
            if(solveQuestion[i]==true)
            {
                cont++;
            }
        }

        if(cont==solveQuestion.Length)
        {
            return Status.end;
        }

        return Status.solved;
    }

    private void generateQuestion()
    {
        Quizz_Status = RandomNumber();

        switch(Quizz_Status)
        {
            case Status.Resolve: QuestionText.text = Question[Ran]; break;
            case Status.solved: generateQuestion();  break;
            case Status.end: EndGame();  break;

        }

        
    }

    private void EndGame()
    {
        QuestionText.text = ("Game Over");
        ResultText.text = ("Gracias por jugar");
    }


    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        Cuadro.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
