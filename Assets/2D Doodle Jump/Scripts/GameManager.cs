using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> platformPool = new List<GameObject>();

    public Transform parent;
    public GameObject platformPrefab;

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


    void SpawnPlatformPool()
    {
        for (int i = 0; i < numberOfPlatform; i++)
        {
            GameObject go = Instantiate(platformPrefab, parent);
            go.SetActive(false);
            go.name = i.ToString();
            platformPool.Add(go);
        }
    }

    void SpawnPlatform(int _index)
    {
        GameObject go = platformPool[_index];
        float randomWeight = Random.Range(0, totalWeight);
        int randomPlatformIndex = GetRamdomPlatform(randomWeight);

        Vector2 platformPos = new Vector2(Random.Range(-7.5f, 7.5f), currentPlatformY);

        switch (randomPlatformIndex)
        {
            case 0:
                go.transform.position = platformPos;
                currentPlatformY += Random.Range(platformSetting.normalPlatform.minHeight, platformSetting.normalPlatform.maxHeight);
                go.name = "0";
                go.SetActive(true);
                break;
            case 1:
                go.transform.position = platformPos;
                currentPlatformY += Random.Range(platformSetting.brokenPlatform.minHeight, platformSetting.brokenPlatform.maxHeight);
                go.name = "1";
                go.SetActive(true);
                break;
            case 2:
                go.transform.position = platformPos;
                currentPlatformY += Random.Range(platformSetting.oncePlatform.minHeight, platformSetting.oncePlatform.maxHeight);
                go.name = "2";
                go.SetActive(true);
                break;
            case 3:
                go.transform.position = platformPos;
                currentPlatformY += Random.Range(platformSetting.doublePlatform.minHeight, platformSetting.doublePlatform.maxHeight);
                go.name = "3";
                go.SetActive(true);
                break;
            case 4:
                go.transform.position = platformPos;
                currentPlatformY += Random.Range(platformSetting.horizontalPlatform.minHeight, platformSetting.horizontalPlatform.maxHeight);
                go.name = "4";
                go.SetActive(true);
                break;
            case 5:
                go.transform.position = platformPos;
                currentPlatformY += Random.Range(platformSetting.verticalPlatform.minHeight, platformSetting.verticalPlatform.maxHeight);
                go.name = "5";
                go.SetActive(true);
                break;
            default:
                break;
        }
    }

    int GetRamdomPlatform(float _weight)
    {
        if (_weight <= platformSetting.normalPlatform.weight)
            return 0;
        else if (_weight <= platformSetting.normalPlatform.weight + platformSetting.brokenPlatform.weight)
            return 1;
        else if (_weight <= platformSetting.normalPlatform.weight + platformSetting.brokenPlatform.weight + platformSetting.oncePlatform.weight)
            return 2;
        else if (_weight <= platformSetting.normalPlatform.weight + platformSetting.brokenPlatform.weight + platformSetting.oncePlatform.weight + platformSetting.doublePlatform.weight)
            return 3;
        else if (_weight <= platformSetting.normalPlatform.weight + platformSetting.brokenPlatform.weight + platformSetting.oncePlatform.weight + platformSetting.doublePlatform.weight + platformSetting.horizontalPlatform.weight)
            return 4;
        else if (_weight <= platformSetting.normalPlatform.weight + platformSetting.brokenPlatform.weight + platformSetting.oncePlatform.weight + platformSetting.doublePlatform.weight + platformSetting.horizontalPlatform.weight + platformSetting.verticalPlatform.weight)
            return 5;
        return -1;
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
}
