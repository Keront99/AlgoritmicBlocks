using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TwoToOneBlock : MonoBehaviour, ILogicalBlock
{
    [SerializeField]
    private InputDot inputDotOne;
    [SerializeField]
    private InputDot inputDotTwo;
    [SerializeField]
    private OutputDot outputDotOne;
    [SerializeField]
    private DotCount dotCount;
    private float numValue1;
    private float numValue2;

    public IDotInput InputDotOne
    {
        get => inputDotOne;
    }
    public IDotInput InputDotTwo
    {
        get => inputDotTwo;
    }

    public IDotOutput OutputDotOne
    {
        get => outputDotOne;
    }
    public IDotOutput OutputDotTwo { get; }

    private void Start()
    {
        inputDotOne.OnInputGet += Calculate;
        inputDotTwo.OnInputGet += Calculate2;
    }

    public void Calculate()
    {
        GetNumbersValues();
        outputDotOne.Send(numValue1);
    }

    public void Calculate2()
    {
        GetNumbersValues();
        outputDotOne.Send(numValue2);
    }

    public void GetNumbersValues()
    {
        numValue1 = inputDotOne.NumberValue;
        numValue2 = inputDotTwo.NumberValue;
        //Debug.Log($"Приняты значения {numValue1} и {numValue2}. Отправили {inputDotOne.IsSended} и {inputDotTwo.IsSended}");
    }

    private enum DotCount
    {
        ONE,
        TWO
    }
}
