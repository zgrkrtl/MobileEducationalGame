using System.IO;
using UnityEngine;
    
public static class SaveManager
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "gameData.json");

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
    }

    public static SaveData Load()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<SaveData>(json);
        }

        Debug.Log("No save file found. Returning new data.");
        return new SaveData();
    }
}