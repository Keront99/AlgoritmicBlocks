using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    string nextLevelNumber;

    private void Start()
    {
        nextLevelNumber = gameObject.name.Substring(11, 3);
    }

    public void OpenNext()
    {
        if (float.Parse(gameObject.name.Substring(13, 1)) == 5)
        {
            SceneManager.LoadScene("MainMenu");
            return;
        }
        SceneManager.LoadScene($"Level{nextLevelNumber}");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene($"Level{float.Parse(gameObject.name.Substring(11, 1))}_{float.Parse(gameObject.name.Substring(13, 1)) - 1}");
    }

    public void RestartSceneSpecial()
    {
        SceneManager.LoadScene($"LevelSpecial");
    }

    public void openMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
