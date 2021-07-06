using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{
    public static void SaveProgress(TheData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/adventure.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("File Saved");
    }
   
    public static TheData LoadData()
    {
        string path = Application.persistentDataPath + "/adventure.txt";

        // Debugging
        Debug.Log(Application.persistentDataPath + "/adventure.txt");


        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            TheData data = formatter.Deserialize(stream) as TheData;
            stream.Close();

            Debug.Log("File Loaded");

            return data;
        }
        else
        {
            Debug.Log("File Not Found");
            TheData temp = new TheData(false, false);
            return temp;
        }        
    }

}
