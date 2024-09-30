using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class SaveManager : GenericSingleton<SaveManager>
{
    public Save loadedSave;

    private const string fileName = "save.data";

    private string Path()
    {
        return Application.persistentDataPath + "/" + fileName;
    }
    

    public void Awake()
    {
        loadedSave.Intialize();
        TryLoad();
    }

    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        DataContractSerializer serializer = new DataContractSerializer(loadedSave.GetType());

        FileStream stream = new FileStream(Path(), FileMode.Create);

        // TODO SaveData

        // formatter.Serialize(stream, save);
        serializer.WriteObject(stream, loadedSave);
        stream.Close();
    }

    public Save Load()
    {
        if(File.Exists(Path()))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            DataContractSerializer serializer = new DataContractSerializer(loadedSave.GetType());

            FileStream stream = new FileStream(Path(), FileMode.Open);

            // Save save = formatter.Deserialize(stream) as Save;
            loadedSave = serializer.ReadObject(stream) as Save;
            stream.Close();

            return loadedSave;
        }
        else
        {
            Debug.LogError("Save file not found in " + Path());
            return null;
        }
    }

    public Save TryLoad()
    {
        if(File.Exists(Path()))
        {
            return Load();
        }
        else
        {
            MakeEmptySave();
            return Load();
        }
    }

    [ContextMenu("Delete Save")]
    public void Delete()
    {
        File.Delete(Path());
    }

    private void MakeEmptySave()
    {
        Vector3 playerPos = new Vector3(12, 10, -58);
        loadedSave.TryAdd<Vector3>("PlayerPosition", playerPos);

        Save();
    }
}
