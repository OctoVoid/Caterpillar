using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    // Easy mode 
    public void EasyMode()
    {
        CaterpillarControl.difficultyPicked = "EASY";
        SceneManager.LoadScene(1);
    }

    // Normal mode 
    public void NormalMode()
    {
        CaterpillarControl.difficultyPicked = "NORMAL";
        SceneManager.LoadScene(1);
    }

    // Hard mode  
    public void HardMode()
    {
        CaterpillarControl.difficultyPicked = "HARD";

        SceneManager.LoadScene(1);
    }
}
