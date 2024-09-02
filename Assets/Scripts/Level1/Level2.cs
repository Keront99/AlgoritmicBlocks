using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Level2 : MonoBehaviour
{
    [SerializeField]
    private PlacementSystem placementSystem;
    [SerializeField]
    private InputDot finishDot;
    [SerializeField]
    private OutputDot startDot;
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

    private AudioSource myFX;
    [SerializeField]
    private AudioClip starsFX;
    private float time;
    private float numValue;
    private int tryCount;
    private int correctCount;
    private System.Random rng = new();

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
        graphFinish.drawGraph(finishDot.NumberValue);

        // Условия победы
        if (Math.Round(finishDot.NumberValue, 2) == Math.Round((numValue * 4) / (numValue * numValue), 2))
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
        inputDots= GetComponentsInChildren<InputDot>();
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
        timer.text = $"{(int)time/60}:{(int)time %60}";
        int price = placementSystem.GetPrice();

        //Debug.Log(price);

        myFX.PlayOneShot(starsFX);

        if (price <= 15)
        {
            stars[0].SetActive(true);
            StartCoroutine(CoroutineStarResize(0.5F, new Vector3(1.2F, 1.2F, 1), stars[0]));
            stars[1].SetActive(true);
            StartCoroutine(CoroutineStarResize(0.5F, new Vector3(1.2F, 1.2F, 1), stars[1]));
            stars[2].SetActive(true);
            StartCoroutine(CoroutineStarResize(0.5F, new Vector3(1.2F, 1.2F, 1), stars[2]));
            return;
        }
        if (price <= 20)
        {
            stars[0].SetActive(true);
            StartCoroutine(CoroutineStarResize(0.5F, new Vector3(1.2F, 1.2F, 1), stars[0]));
            stars[1].SetActive(true);
            StartCoroutine(CoroutineStarResize(0.5F, new Vector3(1.2F, 1.2F, 1), stars[1]));
            return;
        }
        stars[0].SetActive(true);
        StartCoroutine(CoroutineStarResize(0.5F, new Vector3(1.2F, 1.2F, 1), stars[0]));
    }

    public void openMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CloseStoryMenu()
    {
        storyMenu.SetActive(false);
    }

    public void OpenStoryMenu()
    {
        storyMenu.SetActive(true);
    }

    private IEnumerator CoroutineCheck()
    {
        numValue = rng.Next(1, 100);
        startDot.Send(numValue);
        yield return new WaitForSeconds(1);

        graphStart.drawGraph((numValue * 4) / (numValue * numValue));
        Check();
    }

    private IEnumerator CoroutineStarResize(float time, Vector3 target, GameObject star)
    {
        float timer = 0;
        Vector3 @base = star.transform.localScale;
        while (timer < time)
        {
            star.transform.localScale = Vector3.Lerp(@base, target, timer / time);
            yield return null;
            timer += Time.deltaTime;
        }

        timer = 0;
        Vector3 fin = star.transform.localScale;

        while (timer < time)
        {
            star.transform.localScale = Vector3.Lerp(fin, @base, timer / time);
            yield return null;
            timer += Time.deltaTime;
        }
    }
}
