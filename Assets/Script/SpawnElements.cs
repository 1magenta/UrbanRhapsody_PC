using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnElements : MonoBehaviour
{
    public GameObject colaCanPrefab;
    public int initialColaCount = 100; // Number of trash items to spawn initially.
    public Vector3 spawnArea = new Vector3(100f, 0f, 50f); // Area in which trash can be randomly spawned.

    public float itemRegrowthRate = 5.0f;
    public int maxColaCount = 200;

    private void Awake()
    {
    }
    private void Start()
    {
        // Populate the environment with initial food items.
        for (int i = 0; i < initialColaCount; i++)
        {
            Regenerate();
        }

        StartCoroutine(RegenerateCycle());
    }

    IEnumerator RegenerateCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(itemRegrowthRate);
            Regenerate();
            if (CurrentFoodCount() < maxColaCount)
            {
                Debug.Log("New cola can");
                Regenerate();
            }
        }
    }

    // Method to regenerate item at a random position within the spawn area.
    public void Regenerate()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
            0.03f,
            Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
        );

        Instantiate(colaCanPrefab, randomPosition, Quaternion.identity);
    }

    private int CurrentFoodCount()
    {
        return GameObject.FindGameObjectsWithTag("trash").Length;
    }

    public bool IsFoodAvailable()
    {
        if (gameObject != null)
        {
            return true;
        }
        return false;
    }
    public Vector3 GetSpawnArea()
    {
        return spawnArea;
    }
}
