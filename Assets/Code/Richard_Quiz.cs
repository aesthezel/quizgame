using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum GameStatus
{
    Performing,
    Solved,
    End
}
public class Richard_Quiz : MonoBehaviour
{
    public QA_OBJ QA;
    [SerializeField] private TMP_Text textoPregunta;
    [SerializeField] private TMP_Text textoResultado;
    [SerializeField] private TMP_InputField cajaRespuesta;
    [SerializeField] private Button boton;
    private GameStatus status;
    [SerializeField] private GameObject fin;

    public int RandomInt;

    void Start()
    {
        QA.preguntasResueltas = new bool[QA.respuestas.Length];
        GeneratorQA();
        cajaRespuesta.ActivateInputField();
        textoResultado.text = "";
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Confirm();
        }
    }

    public void GeneratorQA()
    {
        status = CheckQ();
        switch (status)
        {
            case GameStatus.Performing:
                textoPregunta.text = QA.preguntas[RandomInt];
                break;

            case GameStatus.Solved:

                GeneratorQA();
                break;

            case GameStatus.End:
                EndGame();

                break;
        }
    }

    public void Confirm()
    {
        if (cajaRespuesta.text.ToLower() == QA.respuestas[RandomInt].ToLower())
        {
            QA.preguntasResueltas[RandomInt] = true;
            StartCoroutine("NextQA", "Correcto!");

        }
        else
        {
            //textoResultado.text = "Erroneo! Intentalo nuevamente...";
            StartCoroutine("NextQA", "Incorrecto!");

        }
    }
    private void EndGame()
    {
        GameObject desTexto = GameObject.Find("confirmar");
        desTexto.SetActive(false);
        GameObject cajainput = GameObject.Find("InputField");
        cajainput.SetActive(false);
        fin.SetActive(true);
        textoPregunta.text = "Preguntas acabadas. Gracias por jugar";
        boton.interactable = false;
        cajaRespuesta.interactable = false;
    }
    private GameStatus CheckQ()
    {
        RandomInt = Random.Range(0, QA.preguntas.Length);
        if (QA.preguntasResueltas[RandomInt] == false)
        {
            //QA.preguntasResueltas[RandomInt] = true;
            return GameStatus.Performing;
        }
        int questionsDone = 0;
        for (int i = 0; i < QA.preguntasResueltas.Length; i++)
        {
            if (QA.preguntasResueltas[i] == true)
            {
                questionsDone++;
            }
        }

        if (questionsDone == QA.preguntasResueltas.Length)
        {
            return GameStatus.End;
        }

        return GameStatus.Solved;
    }
    IEnumerator NextQA(string resultado)
    {
        boton.interactable = false;
        textoResultado.text = resultado;
        yield return new WaitForSeconds(2);
        cajaRespuesta.text = "";
        GeneratorQA();
        textoResultado.text = "";
        cajaRespuesta.ActivateInputField();
        boton.interactable = true;
    }
}
