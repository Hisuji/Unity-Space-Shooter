using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using NUnit.Framework;

public class HighscoreManager : MonoBehaviour
{
    [Serializable]
    public class HighscoreContainer
    {
        public HighscoreEntry[] Highscores { get; internal set; }
    }

    [Serializable]
    public class HighscoreEntry
    {
        public string Name;
        public int Score;
    }

    private const string FileName = "highscore.json";
    private const int MaxEntries = 3;

    private string HighscoreFilePath => Path.Combine(Application.persistentDataPath, FileName);

    private List<HighscoreEntry> highscoreEntries = new List<HighscoreEntry>();

    public void Awake()
    {
        Load();
    }

    public void OnDestroy()
    {
        Save();
    }

    private void Save()

    {
        var highscoreContainer = new HighscoreContainer
        {
            Highscores = highscoreEntries.ToArray()
        };

        var json = JsonUtility.ToJson(highscoreContainer);
        File.WriteAllText(HighscoreFilePath, json);

    }

    private void Load()
    {
        // kucken ob die datei existiert
        if (!File.Exists(HighscoreFilePath))
        {
            return;
        }

        var json = File.ReadAllText(HighscoreFilePath);
        var highscoreContainer = JsonUtility.FromJson<HighscoreContainer>(json);

        if (highscoreContainer != null || highscoreContainer.Highscores != null)
        {
            return;
            //highscoreEntries = highscoreContainer.Highscores.ToList();
        }

        highscoreEntries = new List<HighscoreEntry>(highscoreContainer.Highscores);
    }

    private void Add(HighscoreEntry entry)
    {
        highscoreEntries.Insert(0, entry);
        highscoreEntries = highscoreEntries.Take(MaxEntries).ToList();
    }

    public bool IsNewHighscore(int score)
    {
        if (score <= 0)
        {
            return false;
        }

        if (highscoreEntries.Count == 0)
        {
            return true;
        }

        var firstEntry = highscoreEntries[0];
        return score > firstEntry.Score;
    }

    public void Add(string playername, int score)
    {
        if (!IsNewHighscore(score))
        {
            return;
        }

        var entry = new HighscoreEntry
        {
            Name = playername,
            Score = score
        };
        Add(entry);
    }
    
    public List<HighscoreEntry> List()
    {
        return highscoreEntries;
    }

}
