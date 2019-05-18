using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class PlayerData
{
    private static readonly string filePath = Application.persistentDataPath + "/progress.kw";
    private static int playerSavedLevel = -1;

    public static void Save(int levelId)
    {
        playerSavedLevel = levelId;
        UpdateDataFile();
    }

    public static void Erase()
    {
        playerSavedLevel = 1;
        UpdateDataFile();
    }

    public static int GetProgress()
    {
        if (playerSavedLevel == -1)
        {
            ReadDataFile();
        }
        
        return playerSavedLevel;
    }

    private static void UpdateDataFile()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(filePath);
        binaryFormatter.Serialize(file, playerSavedLevel);
        file.Close();
    }

    private static void ReadDataFile()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            playerSavedLevel = (int) binaryFormatter.Deserialize(file);
            file.Close();
        }
        else
        {
            playerSavedLevel = 1;
        }
    }
}