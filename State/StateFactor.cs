using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Linq;

public class StateFactor : MonoBehaviour
{
    public static StateFactor inst;
    private Dictionary<string, Type> statesByName;
    private bool IsInitialized => statesByName != null;

    private void Awake()
    {
        inst = this;
    }

    private void InitializeFactory()
    {
        if (IsInitialized)
            return;

        var statesTypes = Assembly.GetAssembly(typeof(State)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(State)));

        statesByName = new Dictionary<string, Type>();
        foreach (var type in statesTypes)
        {
            State tempEffect = (State)GetComponent(type);
            statesByName.Add(tempEffect.Name, type);
        }
    }

    public State GetState(string stateType)
    {
        InitializeFactory();

        if (statesByName.ContainsKey(stateType))
        {
            Type type = statesByName[stateType];
            State state = (State)GetComponent(type);
            return state;
        }
        return null;
    }
}
