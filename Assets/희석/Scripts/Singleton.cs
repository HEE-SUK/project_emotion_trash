using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : new()
{
    private static object lockObject = new object();
    private static T instance;

    public static T Instance
    {
        get
        {
            lock (lockObject)
            {
                if (instance == null )
                {
                    instance = new T();
                }
                return instance;
            }
        }
    }
}
