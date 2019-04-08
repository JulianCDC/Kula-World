using UnityEditor;

public class GameManager : Singleton<GameManager>
{
    public int playerScore = 0;
    public int playerJumpLength = 1;
    public float playerSpeed = 1;
    public int maxTime;
    public int elapsedTime = 0;
    public int secondsPerTick = 1;
}