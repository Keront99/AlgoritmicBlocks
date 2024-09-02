using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILogicalBlock
{
    IDotInput InputDotOne { get; }
    IDotInput InputDotTwo { get; }
    IDotOutput OutputDotOne { get; }
    IDotOutput OutputDotTwo { get; }

    void Calculate();
    void GetNumbersValues();
}
