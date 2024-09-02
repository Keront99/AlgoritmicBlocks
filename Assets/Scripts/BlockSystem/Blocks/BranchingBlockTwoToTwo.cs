using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BranchingBlockTwoToTwo : MonoBehaviour
{
    [SerializeField]
    private InputDot inputDotOne;
    [SerializeField]
    private InputDot inputDotTwo;
    [SerializeField]
    private OutputDot outputDotOne;
    [SerializeField]
    private OutputDot outputDotTwo;
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
                {
                    if (numValue1 > numValue2)
                        outputDotOne.Send(numValue1);
                    else
                        outputDotTwo.Send(numValue1);
                    break;
                }
            case 1:
                {
                    if (numValue1 < numValue2)
                        outputDotOne.Send(numValue1);
                    else
                        outputDotTwo.Send(numValue1);
                    break;
                }
            case 2:
                {
                    if (numValue1 >= numValue2)
                        outputDotOne.Send(numValue1);
                    else
                        outputDotTwo.Send(numValue1);
                    break;
                }
            case 3:
                {
                    if (numValue1 <= numValue2)
                        outputDotOne.Send(numValue1);
                    else
                        outputDotTwo.Send(numValue1);
                    break;
                }
            case 4:
                {
                    if (numValue1 == numValue2)
                        outputDotOne.Send(numValue1);
                    else
                        outputDotTwo.Send(numValue1);
                    break;
                }
            case 5:
                {
                    if (numValue1 != numValue2)
                        outputDotOne.Send(numValue1);
                    else
                        outputDotTwo.Send(numValue1);
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