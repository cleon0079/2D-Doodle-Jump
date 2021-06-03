using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> platformPool = new List<GameObject>();

    [SerializeField] GameObject endMenu;
    [SerializeField] GameObject scoreTextGO;
    [SerializeField] Transform cameraPos;
    [SerializeField] Text highScoreText;
    [SerializeField] Text scoreText;
    int score;

    public Transform disableGO;
    public Transform parent;
    public GameObject platformPrefab;

    public GameState gameState = GameState.Game;
    public PlatformSetting platformSetting;

    int numberOfPlatform = 60;
    float totalWeight;
    float currentPlatformY = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetTotalWeight();
        SpawnPlatformPool();

        for (int i = 0; i < numberOfPlatform; i++)
        {
            SpawnPlatform(i);
        }
    }

    void Update()
    {
        score = (int)cameraPos.position.y;
        scoreText.text = "Score: " + score;
    }

    void SpawnPlatformPool()
    {
        for (int i = 0; i < numberOfPlatform; i++)
        {
            GameObject go = Instantiate(platformPrefab, parent);
            go.SetActive(false);
            platformPool.Add(go);
        }
    }

    void SpawnPlatform(int _index)
    {
        GameObject platformGO = platformPool[_index];

        float randomWeight = Random.Range(0, totalWeight);
        PlatformSetting.PlatformType typeOfPlatform = GetRamdomPlatform1(randomWeight);

        Platform platform = platformGO.GetComponent<Platform>();
        platform.SetType(typeOfPlatform);

        Vector2 heightBounds = Vector2.zero;

        switch (typeOfPlatform)
        {
            case PlatformSetting.PlatformType.Normal:
                heightBounds = new Vector2(platformSetting.normalPlatform.minHeight, platformSetting.normalPlatform.maxHeight);
                platformGO.name = PlatformSetting.PlatformType.Normal.ToString();
                platformGO.SetActive(true);
                break;
            case PlatformSetting.PlatformType.Broken:
                heightBounds = new Vector2(platformSetting.brokenPlatform.minHeight, platformSetting.brokenPlatform.maxHeight);
                platformGO.name = PlatformSetting.PlatformType.Broken.ToString();
                platformGO.SetActive(true);
                break;
            case PlatformSetting.PlatformType.Once:
                heightBounds = new Vector2(platformSetting.oncePlatform.minHeight, platformSetting.oncePlatform.maxHeight);
                platformGO.name = PlatformSetting.PlatformType.Once.ToString();
                platformGO.SetActive(true);
                break;
            case PlatformSetting.PlatformType.Doudle:
                heightBounds = new Vector2(platformSetting.doublePlatform.minHeight, platformSetting.doublePlatform.maxHeight);
                platformGO.name = PlatformSetting.PlatformType.Doudle.ToString();
                platformGO.SetActive(true);
                break;
            case PlatformSetting.PlatformType.Horizontal:
                heightBounds = new Vector2(platformSetting.horizontalPlatform.minHeight, platformSetting.horizontalPlatform.maxHeight);
                platformGO.name = PlatformSetting.PlatformType.Horizontal.ToString();
                platformGO.SetActive(true);
                break;
            case PlatformSetting.PlatformType.Vertical:
                heightBounds = new Vector2(platformSetting.verticalPlatform.minHeight, platformSetting.verticalPlatform.maxHeight);
                platformGO.name = PlatformSetting.PlatformType.Vertical.ToString();
                platformGO.SetActive(true);
                break;
            default:
                break; 
        }

        SpriteRenderer goSpriteRend = platformGO.GetComponent<SpriteRenderer>();
        float spriteSize = goSpriteRend.bounds.size.x;
        float leftScreen = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).x;
        float rightScreen = Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x;
        Vector2 platformPos = new Vector2(Random.Range(leftScreen + spriteSize, rightScreen - spriteSize), currentPlatformY);
        currentPlatformY += Random.Range(heightBounds.x, heightBounds.y) + goSpriteRend.bounds.size.y;
        platformGO.transform.position = platformPos;
    }

    void GetTotalWeight()
    {
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
        if(gameState != GameState.End)
        {
            SpawnPlatform(_index);
        }
    }

    public void BackToPool(GameObject _gameObject)
    {
        _gameObject.SetActive(false);
        int index = platformPool.IndexOf(_gameObject);
        NewPlatform(index);
    }

    public void EndGame()
    {
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
