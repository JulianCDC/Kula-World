using System;
using UnityEngine;

public class GameSceneBehaviour : MonoBehaviour
{
    private Map map;
    [SerializeField] private GameObject mapObject;
    public static bool gameOver;
    private static GameObject player;

    void Start()
    {
        gameOver = false;

        LoadMap();
        LoadPlayer();

        InvokeRepeating(nameof(Tick), 0f, 1.0f);
    }

    private void Update()
    {
        if (Input.GetButtonUp("Reset"))
        {
            PlayerDeath();
        }
    }

    void Tick()
    {
        GameManager.Instance.elapsedTime += GameManager.Instance.secondsPerTick;
        Hud.GetHud().timeDisplay.text = $"{GameManager.Instance.elapsedTime} / {GameManager.Instance.maxTime}";

        if (GameManager.Instance.elapsedTime >= GameManager.Instance.maxTime)
        {
            GameOver();
        }
    }

    private void LoadMapMetadata()
    {
        MapMetadata metadata = this.map.metadata;

        GameManager.Instance.maxTime = metadata.timeToFinish > 0 ? metadata.timeToFinish : 600;
        GameManager.Instance.requiredKeys = metadata.numberOfKeys;
    }

    private static void LoadPlayer()
    {
        GameObject playerResource = Resources.Load<GameObject>("Block with player");
        player = Instantiate(playerResource, new Vector3(0, 0, 0), new Quaternion());
    }

    private void LoadMap()
    {
        if (!GameManager.Instance.officialLevel)
        {
            this.map = Map.Load(GameManager.Instance.currentLevel);
            LoadMapIntoScene();
        }
        else
        {
            TextAsset mapAsset = (TextAsset) Resources.Load(GameManager.Instance.currentLevel + ".map");
            this.map = Map.Load(mapAsset);
            LoadMapIntoScene();
        }

        LoadMapMetadata();
    }

    public static void PlayerDeath()
    {
        GameManager.Instance.Death();

        if (GameManager.Instance.TotalScore < 0)
        {
            GameOver();
        }
        else
        {
            Destroy(player);
            LoadPlayer();
        }
    }

    public static void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            DisplayGameOverScreen();
        }
    }

    public static void Win()
    {
        print("Win");
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
            ConfigBlock(blockGameObject, block);
            Instantiate(blockGameObject, mapObject.transform);
        }
    }

    private void ConfigBlock(GameObject blockGameObject, XmlBlock xmlBlock)
    {
        ConfigBlockPosition(blockGameObject.transform, xmlBlock);

        BlockBehaviour blockBehaviour = blockGameObject.GetComponent<BlockBehaviour>();

        if (xmlBlock.hasItem)
        {
            WithItemBehaviour blockWithItemBehaviour = blockGameObject.GetComponent<WithItemBehaviour>();
            blockWithItemBehaviour.itemPosition = xmlBlock.itemPosition;
        }
    }

    private void ConfigBlockPosition(Transform blockTransform, XmlBlock xmlBlock)
    {
        blockTransform.transform.position = new Vector3(xmlBlock.xPos, xmlBlock.yPos, xmlBlock.zPos);
    }
}