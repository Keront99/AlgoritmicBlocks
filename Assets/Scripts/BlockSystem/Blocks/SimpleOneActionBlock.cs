using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimpleOneActionBlock : MonoBehaviour, ILogicalBlock
{
    [SerializeField]
    private InputDot inputDotOne;
    [SerializeField]
    private OutputDot outputDotOne;
    [SerializeField]
    private DotCount dotCount;
    [SerializeField]
    private GameObject indicator;
    private float numValue1;
    private float numValue2;
    private int action;
    private bool reorganized = false;

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
                outputDotOne.Send(numValue1 + numValue2); break;
            case 1:
                outputDotOne.Send(numValue1 - numValue2); break;
            case 2:
                outputDotOne.Send(numValue1 * numValue2); break;
            case 3:
                {
                    if (numValue2 == 0)
                    { 
                        outputDotOne.Send(0); 
                        break; 
                    }
                    outputDotOne.Send(numValue1 / numValue2);
                    break;
                }
            case 4:
                {
                    if (!reorganized)
                    { 
                        outputDotOne.Send(numValue2); 
                        break; 
                    }
                    outputDotOne.Send(numValue1);
                    break;
                }

        }
    }

    public void GetNumbersValues()
    {
        if (inputField.textComponent.text.Length <= 1)
            inputField.text = "0";

        if (!reorganized)
        {
            numValue1 = inputDotOne.NumberValue;
            numValue2 = float.Parse(inputField.text);
        }
        else
        {
            numValue1 = float.Parse(inputField.text);
            numValue2 = inputDotOne.NumberValue;
        }
        action = dropdown.value;
    }

    public void Reorganize()
    {
        if (!reorganized)
        {
            reorganized = true;
            indicator.SetActive(true);
        }
        else
        {
            reorganized = false;
            indicator.SetActive(false);
        }
    }

    private enum DotCount
    {
        ONE,
        TWO
    }
}
