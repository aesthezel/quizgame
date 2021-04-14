using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
    Esto es un enumerador es utilizado como un objeto que solo contiene datos que se pueden evaluar como estados.
    
    Aqui estamos evaluando 3 estados:
    • Performing = 0
    • Solved = 1
    • End = 2

    Ellos pueden ser obtenidos por numeros o por su nombre.
*/

public enum ExampleStatus
{
    Performing,
    Solved,
    End
}

public class Example : MonoBehaviour    //Aqui empieza nuestra clase Example.
{
    [Header("Set-up")]  // Un [Header("")] es un atributo que sirve para colocar un titulo en el inspector con el string que le pases en los parentesis.
                        // El [SerializeField] es un atributo que permite ver variables de acceso privado en el inspector.
    [SerializeField] private ExampleStatus status;  // Como ven aqui se esta adquiriendo una variable de tipo ExampleStatus que es nuestro enum de arriba.
    [SerializeField] private Text questionText;     // Este componente Text es el texto de la UI y se adquiere con la libreria UnityEngine.UI al igual que InputField y Button.
    [SerializeField] private Text resultText;
    [SerializeField] private InputField inputAnswer;
    [SerializeField] private Button confirmButton;
    [SerializeField] private float delayResultTime;

    [Header("Database")]
    [SerializeField] private string[] questions;    // Aqui entramos a las Array (arreglos), es una variable que contiene multiples elementos del mismo tipo.
    [SerializeField] private string[] answers;
    
    private bool[] questionsSolved;                 // Estos elementos no contienen [SerializeField] y son privados debido a que no queremos mostrarlos al inspector u otro componente.
    private int whatQuestionNumber = 0;
    
    private void Start()                            // Metodo Start() proviene de de MonoBehaviour y se ejecuta cuando este componente este en un GameObject y en una escena, y el juego este ejecutandose.
    {
        questionsSolved = new bool[questions.Length];   // Aqui estamos asignandole la misma cantidad de datos de las preguntas, es decir si hay 5 preguntas, tendremos aqui 5 valores bool inicializados en false (porque es su default).
        GenerateQuestion();
    }

    private void GenerateQuestion()                 // Esta funcion genera una nueva pregunta cada vez que se solicita, pero a su vez verifica con otra funcion si la pregunta ya se contesto.
    {
        status = QuestionIsShowed();                // Solicitamos una pregunta aleatoria y verificamos si no ha sido contestada.

        // Con este switch podremos controlar los 3 estados del enum, los cuales podemos direccionar el codigo a 3 posibilidades. 
        switch(status)
        {
            case ExampleStatus.Performing:  // Con Performing hacemos que se muestre la pregunta en la pantalla y el boton de confirmar se apague por unos segundos.
                questionText.text = questions[whatQuestionNumber];
                StartCoroutine("DisableConfirmByTime");
                break;
            case ExampleStatus.Solved:      // Con Solved indicamos que la pregunta que nos genero aleatoriamente en QuestionIsShowed() esta ya contestada, por ello volvemos a ejecutar GenerateQuestion().
                GenerateQuestion();
                break;
            case ExampleStatus.End:         // Si el estado es End, ejecutamos el fin del juego.
                EndGame();
                break;
        }      
    }

    private ExampleStatus QuestionIsShowed()        // Con esta funcion obtenemos el estado del juego verificando si una de las preguntas ha sido contestada exitosamente.
    {
        whatQuestionNumber = Random.Range(0, questions.Length); // A esta variable le asignamos un Random.Range(min, max) esta funcion lo que hace es conseguir un numero aleatorio desde minimo al maximo, y nuestro maximo es la cantidad de preguntas.

        if(questionsSolved[whatQuestionNumber] == false)        // Aqui verificamos si la pregunta que accedemos con el numero aleatorio es "false" (osea no se ha preguntado).
        {
            questionsSolved[whatQuestionNumber] = true;         // Le asignamos true porque no queremos mostrarla mas en el juego, pero si mandarla a ejecutarse en la UI.
            return ExampleStatus.Performing;                    // Este estado indicara que se ejecute esta pregunta en la UI y procedamos a contestarla.
        }

        int questionsDone = 0;
        for (int i = 0; i < questionsSolved.Length; i++)
        {
            if(questionsSolved[i] == true)
            {
                questionsDone++;
            }
        }

        /*
            ¿Que paso en las lineas anteriores?

            El entero que declaramos llamado questionsDone, solo nos servira dentro de nuestra funcion y lo usamos como contador de preguntas resueltas.
            Es asi que, utilizando un "for" iteramos segun la cantidad de preguntas que tenemos asignadas, gracias a questionsSolved.Length.

            Ahora con el if que esta alli presente verifica en cada vuelta si la pregunta esta en "true", si es asi aumentara el contador.

            RECORDAR: que questionSolved es un array de bool, y esta construido segun la cantidad de preguntas que asignamos en el inspector.
        */

        if(questionsDone == questionsSolved.Length) // Esto verifica si el contador anterior es igual al tamano de preguntas, si resulta que son iguales, suponemos que todas las preguntas se han contestado.
        {
            return ExampleStatus.End;   // Como ven aqui retornamos el valor de End, que supondria que el juego ha terminado, pues todas las preguntas han sido contestadas.
        }

        return ExampleStatus.Solved;    // Si ninguna de las condiciones anteriores es validada, llegaremos aqui lo cual suguiere que la pregunta ha sido resuelta y procedera a una condicion que tenemos en QuestionIsShowed().
    }

    public void ConfirmAnswer()     // Con esta funcion verificamos si lo que escribimos en el Input es igual a la respuesta de nuestra pregunta aleatoria.
    {
        if(inputAnswer.text.ToLower() == answers[whatQuestionNumber].ToLower())
        {
            StartCoroutine("ShowResultByTime", "Correcto");     // Esto es una corrutina y sirve para ejecutar una funcion que se rije por un tiempo de ejecucion a parte, bueno para animaciones, contadores, otros...
            GenerateQuestion();                                 // Volvemos a solicitar otra pregunta.
        }
        else
        {
            StartCoroutine("ShowResultByTime", "Incorrecto");   // Si no respondemos bien, nos estancamos hasta poder acertar.
        }

        inputAnswer.text = "";  // Al finalizar de comprobar si es Correcto o Incorrecto vaciamos el texto del Input.
    }

    private void EndGame()  // Cambiamos el texto de las preguntas y el resultado y mostramos los respectivos mensajes de Fin del juego.
    {
        questionText.text = "Fin del juego";
        resultText.text = "Ganaste";
        inputAnswer.interactable = false;       // Deshabilitamos el Input.
        confirmButton.interactable = false;     // Deshabilitamos el Button.
    }

    /*
        COROUTINES / IENUMERATOR

        Son funciones que se rigen en un tiempo a parte de ejeucion, las cuales pueden incluso esperar por un tiempo determinado para luego hacer otra cosa.
        Para lograr eso se le pasa un: 
        
        yield return new WaitForSeconds(time);
        En el caso que queramos esperar tantos segundos dependiendo el tiempo del juego.
    */

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
}