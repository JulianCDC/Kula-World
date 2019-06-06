using System;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private int _playerScore = 0;
    private int _totalScore = 0;

    public int PlayerScore
    {
        get { return this._playerScore; }
        set
        {
            if (Hud.GetHud() != null)
            {
                Hud.GetHud().score.text = value.ToString();
            }

            _playerScore = value;
        }
    }

    public int TotalScore
    {
        get { return _totalScore; }
        private set
        {
            if (Hud.GetHud() != null)
            {
                Hud.GetHud().totalScore.text = value.ToString();
            }

            _totalScore = value;
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
            return;
        }
        
        NewLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetPlayer()
    {
        playerJumpLength = 1;
        playerSpeed = 1;
        secondsPerTick = 1;
        _playerScore = 0;

        foreach (CancellationTokenSource cancellationTokenSource in runningTasksTokens)
        {
            cancellationTokenSource.Cancel();
        }

        runningTasksTokens = new List<CancellationTokenSource>();
    }

    public void Death()
    {
        int currentLevelNumber = 1;
        if (officialLevel)
        {
            currentLevelNumber = int.Parse(currentLevel);
        }
        
        TotalScore -= currentLevelNumber * 50 + PlayerScore;
        PlayerScore = 0;
        ResetPlayer();
    }

    /**
     * Reset used in case of player lose
     */
    public void TotalReset()
    {
        if (officialLevel)
        {
            currentLevel = PlayerData.GetProgress().ToString();
        }

        _playerScore = 0;
        TotalScore = 0;
        maxTime = 600;
        elapsedTime = 0;
        collectedFruits = 0;
        requiredKeys = 0;
        retrievedKeys = 0;
        KeyBehaviour.ResetKeys();
        ResetPlayer();
    }

    public void NewLevel()
    {
        TotalScore += _playerScore;
        _playerScore = 0;
        maxTime = 600;
        elapsedTime = 0;
        collectedFruits = 0;
        requiredKeys = 0;
        retrievedKeys = 0;
        KeyBehaviour.ResetKeys();
        ResetPlayer();
    }
}