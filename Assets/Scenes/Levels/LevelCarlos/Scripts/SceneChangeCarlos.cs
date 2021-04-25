
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeCarlos : MonoBehaviour
{

    public void ChangeSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName); 
    }
   
}
