using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameServices
{
    private static readonly Dictionary<int, object> _serviceMap;

    static GameServices()
    {
        _serviceMap = new Dictionary<int, object>();
    }

    private static int GetId<T>()
    {
        return typeof(T).GetHashCode();
    }

    public static void RegisterService<T>(T service) where T: class
    {
        _serviceMap[GetId<T>()] = service;
    }

    public static void DeregisterService<T>(T service) where T : class
    {
        _serviceMap.Remove(GetId<T>());
    }

    public static T GetService<T>() where T : class
    {
        Debug.Assert(_serviceMap.ContainsKey(GetId<T>()),
            "Trying to get nonexistant service");

        return (T)_serviceMap[GetId<T>()];
    }
}
