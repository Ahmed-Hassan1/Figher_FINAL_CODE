using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public void StartTheGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

