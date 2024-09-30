using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

[System.Serializable]
[KnownType(typeof(float[]))]
[KnownType(typeof(Vector3))]
[DataContract]
public class Save
{
    [DataMember]public Dictionary<string, object> Entrys;

    public Save()
    {
        Intialize();
    }

    public void Intialize()
    {
        Entrys = new Dictionary<string, object>();
    }

    public void TryAdd<T>(string key, T value)
    {
        if(DoesKeyExist(key))
        {
            SetValue<T>(key,value);
            return;
        }

        Entrys.Add(key, value);
    }

    public T TryGetValue<T>(string key)
    {
        if(!DoesKeyExist(key))
        {
            TryAdd<T>(key,default(T));
            return default(T);
        }
        
        T value = (T) Entrys[key];
        return value;
    }

    public void SetValue<T>(string key, T value)
    {
        Entrys[key] = value;
    }

    public void Remove(string key)
    {
        Entrys.Remove(key);
    }

    public bool DoesKeyExist(string key)
    {
        if(Entrys.ContainsKey(key)) return true;
        else return false;
    }
}
