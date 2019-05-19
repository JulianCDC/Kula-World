using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Leaderboard
{
    [Serializable]
    public class Entry
    {
        public string name;
        public int score;
    }

    private static readonly string filePath = Application.persistentDataPath + "/leaderboard.kw";
    private static List<Entry> _entries = new List<Entry>();

    public static void Add(string name, int score)
    {
        _entries.Add(new Entry {name = name, score = score});
        UpdateDataFile();
    }

    public static List<Entry> GetEntriesFrom(int startingEntry, int numberOfEntries)
    {
        if (_entries.Count == 0)
        {
            ReadDataFile();
        }

        return _entries.GetRange(startingEntry, numberOfEntries);
    }

    private static void UpdateDataFile()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(filePath);
        binaryFormatter.Serialize(file, _entries);
        file.Close();
    }

    private static void ReadDataFile()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            _entries = (List<Entry>) binaryFormatter.Deserialize(file);
            file.Close();
        }
    }
}