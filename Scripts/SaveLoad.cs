using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad
{
    public static void SaveObject(string name, object obj)
    {
        string path = Application.persistentDataPath + "/" + name;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);  // if not creates then overwrites
        bf.Serialize(file, obj);
        file.Close();
    }

    public static object LoadObject(string name)
    {
        string path = Application.persistentDataPath + "/" + name;

        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(path);
            object obj = bf.Deserialize(file);
            file.Close();

            return obj;
        }
        else  // First save if no file(s)
        {
            ResetProgress();
            return LoadObject(name);
        }
        
    }

    public static void ResetProgress()
    {
        // Initialize vars
        SaveObject("LEVEL", 0);
        SaveObject("levels", new bool[14]);
        SaveObject("music", 0.5f);
        SaveObject("sound", 1f);
    }
}