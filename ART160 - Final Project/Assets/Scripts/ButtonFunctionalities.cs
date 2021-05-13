using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctionalities : MonoBehaviour
{
    public string nextSceneString; 
    public void StartGame()
    {
        SceneManager.LoadScene(nextSceneString);

    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
