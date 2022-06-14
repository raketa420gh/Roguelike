using System.Collections;
using UnityEngine;

public sealed class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner instance
    {
        get
        {
            if (!_instance)
            {
                var go = new GameObject("CoroutineRunner");
                _instance = go.AddComponent<CoroutineRunner>();
                DontDestroyOnLoad(go);
            }

            return _instance;
        }
    }

    private static CoroutineRunner _instance;

    public static Coroutine StartRoutine(IEnumerator enumerator) => 
        instance.StartCoroutine(enumerator);

    public static void StopRoutine(Coroutine routine) =>
        instance.StopCoroutine(routine);
}