using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevelName;

    //主菜单按键功能
    public void StartGame()
    {
        SceneManager.LoadScene(firstLevelName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
