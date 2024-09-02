using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimpleOneActionTwoToOneBlock : MonoBehaviour, ILogicalBlock
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
    private int action;

    [SerializeField]
    private TMP_Dropdown dropdown;

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
        inputDotTwo.OnInputGet += Calculate;
    }

    public void Calculate()
    {
        GetNumbersValues();
        if (inputDotOne.IsSended != true || inputDotTwo.IsSended != true)
            return;
        switch (action)
        {
            case 0:
                outputDotOne.Send(numValue1 + numValue2); break;
            case 1:
                outputDotOne.Send(numValue1 - numValue2); break;
            case 2:
                outputDotOne.Send(numValue1 * numValue2); break;
            case 3:
                {
                    if (numValue2 == 0)
                    { outputDotOne.Send(0); break; }
                    outputDotOne.Send(numValue1 / numValue2);
                    break;
                }

        }
    }

    public void GetNumbersValues()
    {
        numValue1 = inputDotOne.NumberValue;
        numValue2 = inputDotTwo.NumberValue;
        action = dropdown.value;
        Debug.Log($"Приняты значения {numValue1} и {numValue2}. Отправили {inputDotOne.IsSended} и {inputDotTwo.IsSended}");
    }

    private enum DotCount
    {
        ONE,
        TWO
    }
}
