using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneChanger sceneChanger;

    public void PerformExitGame()
    {
        Debug.Log("Estoy presionando el boton de salir");
        Application.Quit();
    }


}
