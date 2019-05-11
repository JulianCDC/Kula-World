using System;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;

public class GameManager : Singleton<GameManager>
{
    private int _playerScore = 0;
    public int playerScore
    {
        get { return this._playerScore; }
        set
        {
            Hud.GetHud().score.text = value.ToString();
            _playerScore = value; 
        }
    }

    public string currentLevel;
    public bool officialLevel;
    public int playerJumpLength = 1;
    public float playerSpeed = 1;
    public int maxTime;
    public int elapsedTime = 0;
    public int secondsPerTick = 1;
    public int collectedFruits = 0;
    public int requiredKeys = 0;
    public int retrievedKeys = 0;
    public List<CancellationTokenSource> runningTasksTokens = new List<CancellationTokenSource>();

    public bool PlayerHasAllFruits => collectedFruits == 5;

    public void NewLevel()
    {
        Reset();
    }

    public void NextLevel()
    {
        int previousLevel = int.Parse(this.currentLevel);
        if (previousLevel % 5 == 0)
        {
            PlayerData.Save(previousLevel);
        }

        previousLevel += 1;

        if (previousLevel <= Const.OFFICIAL_LEVELS_NUMBER)
        {
            this.currentLevel = previousLevel.ToString();
        }
        else
        {
            GameSceneBehaviour.Win();
        }

        NewLevel();
    }

    private void Reset()
    {
        _playerScore = 0;
        playerJumpLength = 1;
        playerSpeed = 1;
        maxTime = 0;
        elapsedTime = 0;
        secondsPerTick = 1;
        collectedFruits = 0;
        requiredKeys = 0;
        retrievedKeys = 0;

        foreach (CancellationTokenSource cancellationTokenSource in runningTasksTokens)
        {
            cancellationTokenSource.Cancel();
        }
        
        runningTasksTokens = new List<CancellationTokenSource>();
    }
}