using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static NamedGameObject FindNamedGameObjectByName(string name, 
                                                            ref List<NamedGameObject> namedGameObjects)
    {
        for (int i = 0; i < namedGameObjects.Count; i++)
        {
            if(namedGameObjects[i].name == name) 
            {
                return namedGameObjects[i];
            }
        }

        NamedGameObject nullNamedGameObject = new NamedGameObject();
        return nullNamedGameObject;
    }

    public static NamedPlayerObject FindNamedPlayerObjectByName(string name, 
                                                            ref List<NamedPlayerObject> namedPlayerObjects)
    {
        for (int i = 0; i < namedPlayerObjects.Count; i++)
        {
            if(namedPlayerObjects[i].name == name) 
            {
                return namedPlayerObjects[i];
            }
        }

        NamedPlayerObject nullNamedPlayerObject = new NamedPlayerObject();
        return nullNamedPlayerObject;
    }

    public static NamedGameObject FindNamedGameObjectAndRemove(string name, 
                                                                ref List<NamedGameObject> namedGameObjects)
    {
        NamedGameObject objToReturn = new NamedGameObject();
        
        for (int i = 0; i < namedGameObjects.Count; i++)
        {
            if(namedGameObjects[i].name == name) 
            {
                objToReturn = namedGameObjects[i];
                namedGameObjects.RemoveAt(i);
            }
        }
        
        return objToReturn;
    }

    public static NamedPlayerObject FindNamedPlayerObjectAndRemove(string name, 
                                                                ref List<NamedPlayerObject> namedPlayerObjects)
    {
        NamedPlayerObject objToReturn = new NamedPlayerObject();
        
        for (int i = 0; i < namedPlayerObjects.Count; i++)
        {
            if(namedPlayerObjects[i].name == name) 
            {
                objToReturn = namedPlayerObjects[i];
                namedPlayerObjects.RemoveAt(i);
            }
        }
        
        return objToReturn;
    }
}
