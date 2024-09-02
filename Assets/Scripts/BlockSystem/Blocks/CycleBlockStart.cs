using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CycleBlockStart : MonoBehaviour, ILogicalBlock
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
    private bool isCycled;

    [SerializeField]
    private TMP_InputField inputField;

    public IDotInput InputDotOne
    {
        get => inputDotOne;
    }
    public IDotInput InputDotTwo { get; }

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
        numValue2 -= 1;
        if (numValue2 <= 0)
        {
            isCycled = false;
            outputDotTwo.Send(numValue1);
            return;
        }
        outputDotOne.Send(numValue1);
    }

    public void GetNumbersValues()
    {
        if (isCycled)
        {
            numValue1 = inputDotTwo.NumberValue;
            return;
        }
        if (inputField.textComponent.text.Length <= 1)
            inputField.text = "1";
        numValue1 = inputDotOne.NumberValue;
        numValue2 = float.Parse(inputField.text) + 1;
        isCycled = true;
    }

    private enum DotCount
    {
        ONE,
        TWO
    }
}
