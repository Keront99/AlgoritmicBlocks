using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    string levelNumber;

    private void Start()
    {
        levelNumber = gameObject.name.Substring(5, 1);
    }

    public void OpenLevel1()
    {
        SceneManager.LoadScene($"Level{levelNumber}_1");
    }

    public void OpenLevel2()
    {
        SceneManager.LoadScene($"Level{levelNumber}_2");
    }

    public void OpenLevel3()
    {
        SceneManager.LoadScene($"Level{levelNumber}_3");
    }

    public void OpenLevel4()
    {
        SceneManager.LoadScene($"Level{levelNumber}_4");
    }

    public void OpenLevelSpecial()
    {
        SceneManager.LoadScene($"LevelSpecial");
    }
}
