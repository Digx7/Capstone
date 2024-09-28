using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Blackboard : GenericSingleton<UI_Blackboard>
{
    private Dictionary<string, object> Blackboard;

    private void Awake()
    {
        Blackboard = new Dictionary<string, object>();
    }

    public void TryAdd<T>(string key, T value)
    {
        if(DoesKeyExist(key))
        {
            SetValue<T>(key,value);
            return;
        }

        GenericBlackboardElement<T> newEntry = new GenericBlackboardElement<T>();
        newEntry.SetValue(value);
        Blackboard.Add(key, newEntry);
    }

    public T TryGetValue<T>(string key)
    {
        if(!DoesKeyExist(key))
        {
            TryAdd<T>(key,default(T));
            return default(T);
        }
        
        GenericBlackboardElement<T> entry = (GenericBlackboardElement<T>) Blackboard[key];
        return entry.Value;
    }

    public GenericBlackboardElement<T> TryGetEntry<T>(string key)
    {
        if(!DoesKeyExist(key))
        {
            return null;
        }

        return (GenericBlackboardElement<T>) Blackboard[key];
    }

    public void SetValue<T>(string key, T value)
    {
        GenericBlackboardElement<T> entry = (GenericBlackboardElement<T>) Blackboard[key];
        entry.SetValue(value);
    }

    public void Remove(string key)
    {
        Blackboard.Remove(key);
    }

    public bool DoesKeyExist(string key)
    {
        if(Blackboard.ContainsKey(key)) return true;
        else return false;
    }
}
