using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class PlayerData
{
    private static readonly string filePath = Application.persistentDataPath + "/progress.kw";
    private static int playerSavedLevel = -1;
    private static int playerSavedScore = -1;

    public static void Save(int levelId)
    {
        playerSavedLevel = levelId;
        playerSavedScore = GameManager.Instance.TotalScore;
        UpdateDataFile();
    }

    public static void Erase()
    {
        playerSavedLevel = 1;
        playerSavedScore = 0;
        UpdateDataFile();
    }

    public static Dictionary<string, int> GetProgress()
    {
        if (playerSavedLevel == -1 || playerSavedScore == -1)
        {
            ReadDataFile();
        }

        return new Dictionary<string, int>
        {
            {"level", playerSavedLevel},
            {"score", playerSavedScore}
        };
    }

    private static void UpdateDataFile()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(filePath);
        binaryFormatter.Serialize(file, new Dictionary<string, int>
        {
            {"level", playerSavedLevel},
            {"score", playerSavedScore}
        });
        file.Close();
    }

    private static void ReadDataFile()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);

            Dictionary<string, int> savedDatas = (Dictionary<string, int>) binaryFormatter.Deserialize(file);

            playerSavedLevel = savedDatas["level"];
            playerSavedScore = savedDatas["score"];
            file.Close();
        }
        else
        {
            playerSavedLevel = 1;
            playerSavedScore = 0;
        }
    }
}