using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    // 
    public void ExitGame()
    {
        Debug.Log("exitted the game");
        Application.Quit();
    }

    public void StartGame(int level)
    {
        string output = string.Format("loaded level indexed {0}", level);
        Debug.Log("loaded a level");
        SceneManager.LoadScene(level);
    }

}
