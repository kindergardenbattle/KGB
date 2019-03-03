using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void play()
    {
        SceneManager.LoadScene("Game");//build index marche ausse (1)ou faire +1 SceneManager.GetActiveScene().buildIndex+1
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
