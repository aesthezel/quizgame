using UnityEngine;
using UnityEngine.UI;
using System.Collections;





public class ExampleCarlos : MonoBehaviour    
{
    [Header("Set-up")]  // Un [Header("")] es un atributo que sirve para colocar un titulo en el inspector con el string que le pases en los parentesis.
                        
    [SerializeField] private ExampleStatus status;  
    [SerializeField] private Text questionText;     
    [SerializeField] private Text resultText;
    [SerializeField] private InputField inputAnswer;
    [SerializeField] private Button confirmButton;
    [SerializeField] private float delayResultTime;

    [Header("Database")]
    [SerializeField] private string[] questions;    
    [SerializeField] private string[] answers;
    
    private bool[] questionsSolved;                 
    private int whatQuestionNumber = 0;
    public Text myText;

    
    
    private void Start()                            
    {
        questionsSolved = new bool[questions.Length];   
        GenerateQuestion();
        
    }

    private void GenerateQuestion()                 
    {
        status = QuestionIsShowed();                

         
        switch(status)
        {
            case ExampleStatus.Performing:  
                questionText.text = questions[whatQuestionNumber];
                StartCoroutine("DisableConfirmByTime");
                break;
            case ExampleStatus.Solved:      
                GenerateQuestion();
                break;
            case ExampleStatus.End:         
                EndGame();

                break;
        }      
    }

    private ExampleStatus QuestionIsShowed()        
    {
        whatQuestionNumber = Random.Range(0, questions.Length); 

        if(questionsSolved[whatQuestionNumber] == false)        
        {
            questionsSolved[whatQuestionNumber] = true;         
            return ExampleStatus.Performing;                    
        }

        int questionsDone = 0;
        for (int i = 0; i < questionsSolved.Length; i++)
        {
            if(questionsSolved[i] == true)
            {
                questionsDone++;
            }
        }

        
     

        if(questionsDone == questionsSolved.Length) 
        {
            return ExampleStatus.End;   
        }

        return ExampleStatus.Solved;    
    }
    

    public void ConfirmAnswer()     
    {
        if(inputAnswer.text.ToLower() == answers[whatQuestionNumber].ToLower())
        {
            StartCoroutine("ShowResultByTime", "Correcto");     
            GenerateQuestion(); 
             myText.color = Color.green;
             myText.text = "Correcto";                                
        }
        else
        {
            StartCoroutine("ShowResultByTime", "Incorrecto"); 
             myText.color = Color.red;
             myText.text = "Incorrecto";
            
        }

        inputAnswer.text = "";  
    }

    private void EndGame()  
    {
        questionText.text = "Fin del juego";
        resultText.text = "Has Ganado";
        inputAnswer.interactable = false;       
        confirmButton.interactable = false;
        inputAnswer.gameObject.SetActive(false);                                      
    }

   
    private IEnumerator ShowResultByTime(string result)
    {
        resultText.text = result;
        yield return new WaitForSeconds(delayResultTime);
        resultText.text = "";
    }

    private IEnumerator DisableConfirmByTime()
    {
        confirmButton.interactable = false;
        yield return new WaitForSeconds(delayResultTime);
        confirmButton.interactable = true;
    }

    private void Update() 
    {
     
     if(Input.GetKeyDown(KeyCode.Return))
     {
         ConfirmAnswer();
     }   
    }
}