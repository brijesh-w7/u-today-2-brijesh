using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
public class SingletonMono<T> : MonoParent where T : MonoParent
{

    private static T instance = null;
    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(instance);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    public string Classname
    {
        get
        {
            return GetType().Name;
        }
    }

    public static string MethodName
    {
        get
        {
            return  new StackTrace().GetFrame(1).GetMethod().Name;
        }
    }
}
