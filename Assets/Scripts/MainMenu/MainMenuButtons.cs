using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject levelMenu;
    [SerializeField]
    private GameObject level1Menu;
    [SerializeField]
    private GameObject level2Menu;
    [SerializeField]
    private GameObject level3Menu;

    public void openMenuLevel()
    {
        levelMenu.SetActive(true);
        mainMenu.SetActive(false);
        level1Menu.SetActive(false);
        level2Menu.SetActive(false);
        level3Menu.SetActive(false);
    }

    public void openMainMenu()
    {
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void openLevel1Menu()
    {
        levelMenu.SetActive(false);
        level1Menu.SetActive(true);
    }

    public void openLevel2Menu()
    {
        levelMenu.SetActive(false);
        level2Menu.SetActive(true);
    }

    public void openLevel3Menu()
    {
        levelMenu.SetActive(false);
        level3Menu.SetActive(true);
    }


    public void CloseApplication()
    {
        Application.Quit();
    }
}
