using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeGenericClass
{
    public T GenericAssMethod<T>(T parameter)
    {
        Debug.Log($"The parameter is {parameter}");
        return parameter;
    }
}
