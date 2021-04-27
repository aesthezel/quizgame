using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Richard_Quiz : MonoBehaviour
{
    public QA_OBJ _qaPlayer; // Scriptable Object.

    ///////////////////////////////////////////////////////////
    // Text Mesh Pro.

    [SerializeField] private TMP_Text textoPregunta;
    [SerializeField] private TMP_Text textoResultado;
    [SerializeField] private TMP_InputField cajaRespuesta;
    ///////////////////////////////////////////////////////////
    [SerializeField] private Button boton;


    void Start()
    {
        GenerarPreguntaAleatoria();

        cajaRespuesta.ActivateInputField();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmarRespuesta();
        }
    }


    public void ConfirmarRespuesta()
    {
        if (cajaRespuesta.text == _qaPlayer.respuestas[_qaPlayer.preguntaAleatoria])
        {
            textoResultado.text = "Ganaste!";
            StartCoroutine(ReactivadorSiguiente());
        }
        else
        {
            textoResultado.text = "Perdiste!";
            StartCoroutine(ReactivadorSiguiente());
        }
    }


    public void GenerarPreguntaAleatoria()
    {
        _qaPlayer.preguntaAleatoria = Random.Range(0, _qaPlayer.preguntas.Length); // Genera un numero aleatorio el cual se asignará para una array.
        textoPregunta.text = _qaPlayer.preguntas[_qaPlayer.preguntaAleatoria];      // Asigna una pregunta de la array "preguntas" usando el numero aleatorio de preguntas.
        textoResultado.text = "";
    }

    IEnumerator ReactivadorSiguiente()
    {
        boton.interactable = false;
        yield return new WaitForSeconds(2);
        cajaRespuesta.text = "";
        GenerarPreguntaAleatoria();
        cajaRespuesta.ActivateInputField();
        boton.interactable = true;


    }


}
