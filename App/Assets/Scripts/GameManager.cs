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

    public int playerJumpLength = 1;
    public float playerSpeed = 1;
    public int maxTime;
    public int elapsedTime = 0;
    public int secondsPerTick = 1;
    public int collectedFruits = 0;

    public bool PlayerHasAllFruits => collectedFruits == 5;

    public void NewLevel()
    {
        Reset();
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
    }
}