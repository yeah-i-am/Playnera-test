#if !UNITY_EDITOR
//#define EXTERNAL_INITIALIZATION
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class SelfInstatiateSingleton<T> : Singleton<T> where T : Component
{
    protected virtual void OnInstatiate()
    {
    }
}

public static class SelfInstatiateSigleton
{
#if !EXTERNAL_INITIALIZATION
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        foreach (float progress in InitializeSingleton())
            ;

        InitializeSingleton();
    }
#endif

    public static IEnumerable<float> InitializeSingleton()
    {
        Type[] types =
             Assembly.GetExecutingAssembly().GetTypes()
             .Where(t => t.IsSubclassOfRawGeneric(typeof(SelfInstatiateSingleton<>))).ToArray();

        for (int i = 0; i < types.Length; i++)
        {
            Type type = types[i];
            object instance = type.GetProperty("Instance", BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Public).GetValue(null);
            MethodInfo onInit = type.GetMethod("OnInstatiate", BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic);

            onInit.Invoke(instance, null);

            yield return (float)i / types.Length;
        }

        yield return 1;
    }
}
