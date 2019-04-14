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
}