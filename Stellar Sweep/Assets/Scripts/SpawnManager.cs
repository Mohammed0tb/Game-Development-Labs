using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private float initialSpawnInterval = 2.0f;
    [SerializeField] private float minimumSpawnInterval = 0.4f;
    [SerializeField] private float initialAsteroidSpeed = 5f;
    [SerializeField] private float maxAsteroidSpeed = 15f;
    [SerializeField] private float difficultyIncreaseRate = 0.05f; 

    private float currentSpawnInterval;
    private float currentSpeed;
    private float arenaSize = 25f; // Matches arena bounds
    private bool isGameOver = false;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        currentSpeed = initialAsteroidSpeed;

        StartCoroutine(SpawnAsteroids());

        StartCoroutine(IncreaseDifficulty());
    }

    private IEnumerator SpawnAsteroids()
    {
        yield return new WaitForSeconds(1f);

        while (!isGameOver)
        {
            SpawnOneAsteroid();
            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }

    private IEnumerator IncreaseDifficulty()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(3f);

            currentSpawnInterval = Mathf.Max(minimumSpawnInterval,
                currentSpawnInterval - difficultyIncreaseRate);

            currentSpeed = Mathf.Min(maxAsteroidSpeed,
                currentSpeed + 0.2f);
        }
    }

    private void SpawnOneAsteroid()
    {
        int randomIndex = Random.Range(0, asteroidPrefabs.Length);
        GameObject prefab = asteroidPrefabs[randomIndex];

        Vector3 spawnPos = GetRandomEdgePosition();

        Vector3 targetPos = new Vector3(
            Random.Range(-arenaSize * 0.5f, arenaSize * 0.5f),
            0.5f,
            Random.Range(-arenaSize * 0.5f, arenaSize * 0.5f)
        );
        Vector3 direction = (targetPos - spawnPos).normalized;

        GameObject asteroid = Instantiate(prefab, spawnPos, Quaternion.identity);
        asteroid.GetComponent<Asteroid>().Initialize(direction, currentSpeed);
    }

    private Vector3 GetRandomEdgePosition()
    {
        int edge = Random.Range(0, 4);
        float randomPos = Random.Range(-arenaSize, arenaSize);
        float y = 0.5f; 

        switch (edge)
        {
            case 0: return new Vector3(-arenaSize, y, randomPos);
            case 1: return new Vector3(arenaSize, y, randomPos);  
            case 2: return new Vector3(randomPos, y, -arenaSize); 
            case 3: return new Vector3(randomPos, y, arenaSize);  
            default: return new Vector3(-arenaSize, y, randomPos);
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        StopAllCoroutines();
        Debug.Log("Spawning stopped — Game Over.");
    }
}