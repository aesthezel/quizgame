using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
using TMPro;

public class Quiz_godiegogt : MonoBehaviour
{
    public TMP_Text questiontext;
    public TMP_Text answertext;
    public TMP_InputField answerField;

    public string [] questions;
    public string [] answers;
    // public Button confirmeButton;
    // Start is called before the first frame update
   int randomquestion;
    void Start()
    {
       GenerateQuestion();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfirmeAnswer(){
        if (answerField.text==answers[randomquestion])
        {
            answertext.text="Correcto";
            GenerateQuestion();
        }else
        {
            answertext.text="incorrecto";
        }
    }
public void GenerateQuestion(){
    randomquestion = Random.Range(0,questions.Length);
     questiontext.text = questions[randomquestion];
}


}

public enum QuizStates_godiego
{
    Resolving,
    Solved,
    End
}

