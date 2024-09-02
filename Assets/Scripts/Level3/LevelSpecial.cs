using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpecial : MonoBehaviour
{
    [SerializeField]
    private PlacementSystem placementSystem;
    [SerializeField]
    private InputDot finishDotOne;
    [SerializeField]
    private OutputDot startDotOne;
    //[SerializeField]
    //private OutputDot startDotTwo;
    [SerializeField]
    private GameObject victoryMenu;
    [SerializeField]
    private GraphCreator graphStart;
    [SerializeField]
    private GraphCreator graphFinish;
    [SerializeField]
    private List<GameObject> stars;
    [SerializeField]
    private TextMeshProUGUI timer;
    [SerializeField]
    private GameObject darkness;
    [SerializeField]
    private GameObject storyMenu;
    [SerializeField]
    private TMP_InputField formulaText;
    [SerializeField]
    private GameObject errorFormula;
    [SerializeField]
    private TextMeshProUGUI textBoxFormula;

    private AudioSource myFX;
    [SerializeField]
    private AudioClip starsFX;
    private float time;
    private float x, numValue2;
    private string formula = "x";
    private int tryCount;
    private int correctCount;
    private System.Random rng = new();

    private DataTable dt = new();

    private void Start()
    {
        myFX = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
    }

    private void Check()
    {
        //Debug.Log("Проверка");
        //Debug.Log(finishDot.NumberValue);
        //Debug.Log(numValue);

        graphFinish.drawGraph(finishDotOne.NumberValue);

        // Условия победы
        if (finishDotOne.NumberValue == x)
        {
            //Debug.Log("+1 к победе");
            correctCount += 1;
        }

        //Debug.Log(correctCount);

        if (correctCount == 10)
        {
            Victory();
            darkness.SetActive(false);
            return;
        }

        InputDot[] inputDots;
        inputDots = GetComponentsInChildren<InputDot>();
        foreach (InputDot dot in inputDots)
        {
            dot.Reset();
        }

        //Debug.Log($"Количество попыток: {tryCount}, верных: {correctCount}");
        if (tryCount < 10)
        {
            StartCheck();
        }
        else
        {
            tryCount = 0;
            correctCount = 0;
            darkness.SetActive(false);
        }

    }

    public void StartCheck()
    {
        //Debug.Log("Начало проверки");
        tryCount += 1;
        darkness.SetActive(true);
        StartCoroutine(CoroutineCheck());
    }

    private void Victory()
    {
        victoryMenu.SetActive(true);
        timer.text = $"{(int)time / 60}:{(int)time % 60}";
        int price = placementSystem.GetPrice();

        //Debug.Log(price);

        myFX.PlayOneShot(starsFX);
    }

    public void openMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CloseStoryMenu()
    {
        formula = formulaText.text;
        string tempString = formula;
        tempString = tempString.Replace("x", $"{x}");

        try
        {
            x = float.Parse((dt.Compute(tempString, "")).ToString());
        }
        catch
        {
            errorFormula.SetActive(true);
            throw;
        }

        textBoxFormula.text = "y = " + formula;
        storyMenu.SetActive(false);
    }

    public void OpenStoryMenu()
    {
        storyMenu.SetActive(true);
    }

    private IEnumerator CoroutineCheck()
    {
        string tempString = formula;
        x = rng.Next(1, 100);
        startDotOne.Send(x);

        yield return new WaitForSeconds(1);

        tempString = tempString.Replace("x", $"{x}");

        x = float.Parse((dt.Compute(tempString, "")).ToString());

        graphStart.drawGraph(x);
        Check();
    }
}
