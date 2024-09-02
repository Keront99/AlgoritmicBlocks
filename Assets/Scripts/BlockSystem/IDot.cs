using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDot
{
    float NumberValue { get; }
    private enum DotIndexes
    {
        ONE,
        TWO
    }
}

public interface IDotInput : IDot
{
    public void Activate(float numVal);
    bool IsSended { get; }
    public event Action OnInputGet;
}

public interface IDotOutput : IDot
{
    public List<InputDot> InputDots { get; }
    void Send(float input);
}