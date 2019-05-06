using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneBehaviour : MonoBehaviour
{
    private Map map;
    [SerializeField] private string mapName;
    [SerializeField] private GameObject mapObject;
    private static bool gameOver;
    
    void Start()
    {
        ResetLevel();

        LoadMap();
        LoadPlayer();
        
        GameManager.Instance.maxTime = 600;
        InvokeRepeating(nameof(Tick), 0f, 1.0f);
    }

    private static void ResetLevel()
    {
        GameManager.Instance.NewLevel();
        gameOver = false;
    }
    
    void Tick()
    {
        GameManager.Instance.elapsedTime += GameManager.Instance.secondsPerTick;

        if (GameManager.Instance.elapsedTime >= GameManager.Instance.maxTime)
        {
            GameSceneBehaviour.GameOver();
        }
    }

    private void LoadPlayer()
    {
        GameObject player = Resources.Load<GameObject>("Block with player");
        Instantiate(player, new Vector3(0, 0, 0), new Quaternion());
    }
    
    private void LoadMap()
    {
        this.map = Map.Load(this.mapName);
        LoadMapIntoScene();
    }

    public static void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            DisplayGameOverScreen();
        }
    }

    private static void DisplayGameOverScreen()
    {
        var gameOverPopup = Resources.Load<GameObject>("GameOverPopup");
        Instantiate(gameOverPopup, Hud.GetHud().transform);
    }

    private void LoadMapIntoScene()
    {
        foreach (XmlBlock block in this.map.blocks)
        {
            GameObject blockGameObject = Resources.Load<GameObject>(block.objectType);
            configBlock(blockGameObject, block);
            Instantiate(blockGameObject, mapObject.transform);
        }
    }

    private void configBlock(GameObject blockGameObject, XmlBlock xmlBlock)
    {
        configBlockPosition(blockGameObject.transform, xmlBlock);
        
        BlockBehaviour blockBehaviour = blockGameObject.GetComponent<BlockBehaviour>();

        if (xmlBlock.hasItem)
        {
            WithItemBehaviour blockWithItemBehaviour = blockGameObject.GetComponent<WithItemBehaviour>();
        }
    }

    private void configBlockPosition(Transform blockTransform, XmlBlock xmlBlock)
    {
        blockTransform.transform.position = new Vector3(xmlBlock.xPos, xmlBlock.yPos, xmlBlock.zPos);
    }
}
