using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileContentSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject obstaclePrefab;

    public float laneDistance = 3f;

    void Start()
    {
        for (int i = -1; i <= 1; i++)
        {
            float rand = Random.value;

            Vector3 spawnPos = new Vector3(i * laneDistance, 1, transform.position.z + 2);

            if (rand < 0.5f)
                Instantiate(coinPrefab, spawnPos, Quaternion.identity, transform);
            else if (rand < 0.8f)
                Instantiate(obstaclePrefab, spawnPos, Quaternion.identity, transform);
        }
    }

}
