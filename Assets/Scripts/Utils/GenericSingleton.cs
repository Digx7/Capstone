using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : Component
{
    // create a private reference to T instance
    private static T instance;

    public static T Instance
    {
        get
        {
            // if instance is null
            if (instance == null)
            {
                // find the generic instance
                instance = FindObjectOfType<T>();

                // if it's null again create a new object
                // and attach the generic instance
                // if (instance == null)
                // {
                //     GameObject obj = new GameObject();
                //     obj.name = typeof(T).Name;
                //     instance = obj.AddComponent<T>();
                // }
            }
            return instance;
        }
    }
    
    public static bool IsInstanceNull()
    {
        if(instance == null) return true;
        else return false;
    }

    public virtual void Awake()
    {
        // create the instance
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("A Singleton: " + this.gameObject.name + "is being destroyed because another one (" + instance.gameObject.name + ") already exists");
            Destroy(gameObject);
        }
    }
}
