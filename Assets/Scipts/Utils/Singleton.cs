using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance => GetInstance();

    protected static T GetInstance()
    {
        if (instance != null || Game.IsAppQuitting)
            return instance;

        T[] objects = FindObjectsOfType<T>();

        if (objects.Length == 1)
        {
            instance = objects[0];
            DontDestroyOnLoad(instance);

            return instance;
        }
        else if (objects.Length > 1)
        {
            Debug.LogError($"There is more than one {typeof(T).Name} in the scene. But this is Singleton! All instances will be destroyed.");

            for (int i = 0; i < objects.Length; i++)
                Destroy(objects[i]);
        }

        GameObject gameObject = new GameObject(typeof(T).Name);

        instance = gameObject.AddComponent<T>();

        DontDestroyOnLoad(gameObject);

        return instance;
    }
}

public static class Game
{
    public static bool IsAppQuitting { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    private static void Yes()
    {
        Application.quitting += () => IsAppQuitting = true;
    }
}
