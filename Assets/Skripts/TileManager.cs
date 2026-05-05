using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject tilePrefab;
    public GameObject coinPrefab;
    public GameObject lifePotionPrefab;
    public GameObject[] obstaclePrefabs;
    public GameObject[] backgroundPrefabs;

    [Header("Settings")]
    public int tilesOnScreen = 5;
    public float tileLength = 10f;
    public Transform player;

    [Header("Speed Settings")]
    public float gameSpeed = 1.0f;
    public float speedIncreasePerMinute = 0.2f;

    [Header("Background Settings")]
    public float backgroundOffset = 0f;

    [Header("Destroy Settings")]
    public float destroyDistance = 20f;

    private float spawnZ = 0;
    private List<GameObject> activeTiles = new List<GameObject>();
    private int lastObstacleIndex = -1;
    private int spawnedTilesCount = 0;

    private float timeSinceStart = 0f;

    void Start()
    {
        for (int i = 0; i < tilesOnScreen; i++)
            SpawnTile();
    }

    void Update()
    {
        timeSinceStart += Time.deltaTime;

        if (timeSinceStart >= 60f)
        {
            timeSinceStart -= 60f;
            gameSpeed += speedIncreasePerMinute;

        }

        if (player.position.z - destroyDistance > spawnZ - tilesOnScreen * tileLength)
        {
            SpawnTile();
            DeleteTile();
        }
    }

    void SpawnTile()
    {
        GameObject go = Instantiate(tilePrefab, Vector3.forward * spawnZ, Quaternion.identity);
        activeTiles.Add(go);

        SpawnBackground(spawnZ + backgroundOffset, go.transform);

        if (spawnedTilesCount >= 2)
        {
            SpawnContent(go);
        }

        spawnedTilesCount++;
        spawnZ += tileLength;
    }

    void DeleteTile()
    {
        if (activeTiles.Count > 0)
        {
            Destroy(activeTiles[0]);
            activeTiles.RemoveAt(0);
        }
    }

    void SpawnBackground(float zPosition, Transform parentTile)
    {
        if (backgroundPrefabs != null && backgroundPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, backgroundPrefabs.Length);
            GameObject bgPrefab = backgroundPrefabs[randomIndex];

            Vector3 spawnPosition = new Vector3(0, 0, zPosition);
            GameObject bgInstance = Instantiate(bgPrefab, spawnPosition, Quaternion.identity);

            bgInstance.transform.SetParent(parentTile, true);
        }
    }

    void SpawnContent(GameObject tileObj)
    {
        Tile tile = tileObj.GetComponent<Tile>();
        if (tile != null)
        {
            SpawnLane(tile.leftPoint, tileObj.transform);
            SpawnLane(tile.centerPoint, tileObj.transform);
            SpawnLane(tile.rightPoint, tileObj.transform);
        }
    }

    void SpawnLane(Transform point, Transform tileParent)
    {
        if (point == null) return;

        float rand = Random.value;
        GameObject obj = null;

        if (rand < 0.01f)
        {
            obj = Instantiate(lifePotionPrefab, point.position, Quaternion.identity);
        }
        else if (rand < 0.30f)
        {
            obj = Instantiate(coinPrefab, point.position, Quaternion.identity);
        }
        else if (rand < 0.60f)
        {
            if (obstaclePrefabs != null && obstaclePrefabs.Length > 1)
            {
                int randomIndex = lastObstacleIndex;

                while (randomIndex == lastObstacleIndex)
                {
                    randomIndex = Random.Range(0, obstaclePrefabs.Length);
                }

                lastObstacleIndex = randomIndex;

                Quaternion rot = GetRandomRotation();
                obj = Instantiate(obstaclePrefabs[randomIndex], point.position, rot);
            }
            else if (obstaclePrefabs.Length == 1)
            {
                obj = Instantiate(obstaclePrefabs[0], point.position, GetRandomRotation());
            }
        }

        if (obj != null)
        {
            obj.transform.SetParent(tileParent, true);
        }
    }

    Quaternion GetRandomRotation()
    {
        int angle = Random.Range(0, 4) * 90;
        return Quaternion.Euler(0, angle, 0);
    }
}
