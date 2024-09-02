using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BranchingBlock : MonoBehaviour, ILogicalBlock
{
    [SerializeField]
    private InputDot inputDotOne;
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
    }

    public void Calculate()
    {
        GetNumbersValues();
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
        if (inputField.textComponent.text.Length <= 1)
            inputField.text = "0";
        numValue1 = inputDotOne.NumberValue;
        numValue2 = float.Parse(inputField.text);
        action = dropdown.value;
    }

    private enum DotCount
    {
        ONE,
        TWO
    }
}
