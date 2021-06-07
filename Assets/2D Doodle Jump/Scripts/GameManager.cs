using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Make a object pool for the platform
    public List<GameObject> platformPool = new List<GameObject>();

    // EndMenu and scoretext gameobject
    [SerializeField] GameObject endMenu;
    [SerializeField] GameObject scoreTextGO;
    // Get the cameras midpoint pos
    [SerializeField] Transform cameraPos;
    // High score text and gameplay score text
    [SerializeField] Text highScoreText;
    [SerializeField] Text scoreText;
    // Score of the game
    int score;

    // The disable cubes pos and the platforms spawning parent
    public Transform disableGO;
    public Transform parent;
    // Prefab of the platforms
    public GameObject platformPrefab;

    public GameState gameState = GameState.Game;
    public PlatformSetting platformSetting;

    // How many platform is in the object pool
    int numberOfPlatform = 60;
    // Total weight to count the % to spawn platforms
    float totalWeight;
    // The current platforms Y pos
    float currentPlatformY = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Get the total weight and then spawn the platform object pool
        GetTotalWeight();
        SpawnPlatformPool();

        // Spawn random type of platform in the scene
        for (int i = 0; i < numberOfPlatform; i++)
        {
            SpawnPlatform(i);
        }
    }

    void Update()
    {
        // Show the text in the canves
        score = (int)cameraPos.position.y;
        scoreText.text = "Score: " + score;
    }

    void SpawnPlatformPool()
    {
        // Spawn platform object pool in a list
        for (int i = 0; i < numberOfPlatform; i++)
        {
            GameObject go = Instantiate(platformPrefab, parent);
            go.SetActive(false);
            platformPool.Add(go);
        }
    }

    void SpawnPlatform(int _index)
    {
        // Get the platform we are spawning
        GameObject platformGO = platformPool[_index];

        // Get a random platform type
        float randomWeight = Random.Range(0, totalWeight);
        PlatformSetting.PlatformType typeOfPlatform = GetRamdomPlatform1(randomWeight);

        // Set the type of this platform
        Platform platform = platformGO.GetComponent<Platform>();
        platform.SetType(typeOfPlatform);

        Vector2 heightBounds = Vector2.zero;
        switch (typeOfPlatform)
        {
            case PlatformSetting.PlatformType.Normal:
                // How height is this platform spawning then the current platform, Set the name as its type, Make it shows in the scene, Set its jumpheight and gravity
                heightBounds = new Vector2(platformSetting.normalPlatform.minHeight, platformSetting.normalPlatform.maxHeight);
                platformGO.name = PlatformSetting.PlatformType.Normal.ToString();
                platformGO.SetActive(true);
                platform.SetJumpHeight(platformSetting.normalPlatform.jumpHeight);
                platform.SetGravity(platformSetting.normalPlatform.gravity);
                break;
            case PlatformSetting.PlatformType.Broken:
                // How height is this platform spawning then the current platform, Set the name as its type, Make it shows in the scene, Set its jumpheight and gravity
                heightBounds = new Vector2(platformSetting.brokenPlatform.minHeight, platformSetting.brokenPlatform.maxHeight);
                platformGO.name = PlatformSetting.PlatformType.Broken.ToString();
                platformGO.SetActive(true);
                platform.SetJumpHeight(platformSetting.brokenPlatform.jumpHeight);
                platform.SetGravity(platformSetting.brokenPlatform.gravity);
                break;
            case PlatformSetting.PlatformType.Once:
                // How height is this platform spawning then the current platform, Set the name as its type, Make it shows in the scene, Set its jumpheight and gravity
                heightBounds = new Vector2(platformSetting.oncePlatform.minHeight, platformSetting.oncePlatform.maxHeight);
                platformGO.name = PlatformSetting.PlatformType.Once.ToString();
                platformGO.SetActive(true);
                platform.SetJumpHeight(platformSetting.oncePlatform.jumpHeight);
                platform.SetGravity(platformSetting.oncePlatform.gravity);
                break;
            case PlatformSetting.PlatformType.Doudle:
                // How height is this platform spawning then the current platform, Set the name as its type, Make it shows in the scene, Set its jumpheight and gravity
                heightBounds = new Vector2(platformSetting.doublePlatform.minHeight, platformSetting.doublePlatform.maxHeight);
                platformGO.name = PlatformSetting.PlatformType.Doudle.ToString();
                platformGO.SetActive(true);
                platform.SetJumpHeight(platformSetting.doublePlatform.jumpHeight);
                platform.SetGravity(platformSetting.doublePlatform.gravity);
                break;
            case PlatformSetting.PlatformType.Horizontal:
                // How height is this platform spawning then the current platform, Set the name as its type, Make it shows in the scene, Set its jumpheight and gravity
                heightBounds = new Vector2(platformSetting.horizontalPlatform.minHeight, platformSetting.horizontalPlatform.maxHeight);
                platformGO.name = PlatformSetting.PlatformType.Horizontal.ToString();
                platformGO.SetActive(true);
                platform.SetJumpHeight(platformSetting.horizontalPlatform.jumpHeight);
                platform.SetGravity(platformSetting.horizontalPlatform.gravity);
                break;
            case PlatformSetting.PlatformType.Vertical:
                // How height is this platform spawning then the current platform, Set the name as its type, Make it shows in the scene, Set its jumpheight and gravity
                heightBounds = new Vector2(platformSetting.verticalPlatform.minHeight, platformSetting.verticalPlatform.maxHeight);
                platformGO.name = PlatformSetting.PlatformType.Vertical.ToString();
                platformGO.SetActive(true);
                platform.SetJumpHeight(platformSetting.verticalPlatform.jumpHeight);
                platform.SetGravity(platformSetting.verticalPlatform.gravity);
                break;
            default:
                break; 
        }

        // Get the size of the sprite and the leftscreen and rightscreen X
        SpriteRenderer goSpriteRend = platformGO.GetComponent<SpriteRenderer>();
        float spriteSize = goSpriteRend.bounds.size.x;
        float leftScreen = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).x;
        float rightScreen = Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x;

        // Put the platform in the random place with in the screen range
        Vector2 platformPos = new Vector2(Random.Range(leftScreen + spriteSize, rightScreen - spriteSize), currentPlatformY);
        // Every time we spawn a platform we get its Y pos and next time we spawn we add the bounds to it
        currentPlatformY += Random.Range(heightBounds.x, heightBounds.y) + goSpriteRend.bounds.size.y;
        // Place the platform
        platformGO.transform.position = platformPos;
    }

    void GetTotalWeight()
    {
        // Get the total weight
        float weight = 0;
        weight += platformSetting.normalPlatform.weight;
        weight += platformSetting.brokenPlatform.weight;
        weight += platformSetting.oncePlatform.weight;
        weight += platformSetting.doublePlatform.weight;
        weight += platformSetting.horizontalPlatform.weight;
        weight += platformSetting.verticalPlatform.weight;
        totalWeight = weight;
    }

    PlatformSetting.PlatformType GetRamdomPlatform1(float _weight)
    {
        // Get a random type of the platform
        if (_weight <= platformSetting.normalPlatform.weight)
            return PlatformSetting.PlatformType.Normal;
        else if (_weight <= platformSetting.normalPlatform.weight + platformSetting.brokenPlatform.weight)
            return PlatformSetting.PlatformType.Broken;
        else if (_weight <= platformSetting.normalPlatform.weight + platformSetting.brokenPlatform.weight + platformSetting.oncePlatform.weight)
            return PlatformSetting.PlatformType.Once;
        else if (_weight <= platformSetting.normalPlatform.weight + platformSetting.brokenPlatform.weight + platformSetting.oncePlatform.weight + platformSetting.doublePlatform.weight)
            return PlatformSetting.PlatformType.Doudle;
        else if (_weight <= platformSetting.normalPlatform.weight + platformSetting.brokenPlatform.weight + platformSetting.oncePlatform.weight + platformSetting.doublePlatform.weight + platformSetting.horizontalPlatform.weight)
            return PlatformSetting.PlatformType.Horizontal;
        else if (_weight <= platformSetting.normalPlatform.weight + platformSetting.brokenPlatform.weight + platformSetting.oncePlatform.weight + platformSetting.doublePlatform.weight + platformSetting.horizontalPlatform.weight + platformSetting.verticalPlatform.weight)
            return PlatformSetting.PlatformType.Vertical;
        return default;
    }

    void NewPlatform(int _index)
    {
        // If the game is still running then spawn a new platform
        if(gameState != GameState.End)
        {
            SpawnPlatform(_index);
        }
    }

    public void BackToPool(GameObject _gameObject)
    {
        // Put the disable platform back to the pool
        _gameObject.SetActive(false);
        int index = platformPool.IndexOf(_gameObject);
        NewPlatform(index);
    }

    public void EndGame()
    {
        // End the game and show the highscore and save the highscore
        if(gameState != GameState.End)
        {
            gameState = GameState.End;    
            if(PlayerPrefs.HasKey("Score"))
            {
                int highscore = PlayerPrefs.GetInt("Score");
                if(highscore <= score)
                {
                    PlayerPrefs.SetInt("Score", score);
                }
                else
                {
                    PlayerPrefs.SetInt("Score", highscore);
                }
            }
            else
            {
                PlayerPrefs.SetInt("Score", score);
            }
            scoreTextGO.SetActive(false);
            endMenu.SetActive(true);
            highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("Score");
        }
    }
}

public enum GameState
{
    Game,
    End
}
