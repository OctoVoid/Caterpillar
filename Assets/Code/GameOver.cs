using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    // Yes
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    // No
    public void QuitGame()
    {
        Application.Quit();
    }
}
