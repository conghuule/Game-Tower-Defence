using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]

    [SerializeField] private int level;
    [SerializeField] private int baseEnemies = 10;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 2.0f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    public int totalWaves = 3;
    private float timeSinceLastWave;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());

    }
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning) return;

        timeSinceLastWave += Time.deltaTime;

        if (timeSinceLastWave >= (difficultyScalingFactor * enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastWave = 0f;
        }

        if (enemiesLeftToSpawn == 0 && enemiesAlive == 0)
        {
            EndWave();
        }
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastWave = 0f;
        if (currentWave == totalWaves && enemiesAlive == 0)
        {
            // Win
            Debug.Log("You win");
            LevelManager.main.gameResult.SetActive(true);
        }
        else
        {
            currentWave++;
            StartCoroutine(StartWave());
        }

    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = currentWave == 1 ? enemyPrefabs[0] : enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        float maxHealth = GetMaxHealthForPrefab(prefabToSpawn);
        GameObject spawnedEnemy = Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);

        Enemy enemyScript = spawnedEnemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.SetMaxHealth(maxHealth);
        }
    }
    private float GetMaxHealthForPrefab(GameObject prefab)
    {
        // Set max health based on the prefab
        if (level == 1)
        {
            if (prefab == enemyPrefabs[0]) return 30f;
            if (prefab == enemyPrefabs[1]) return 60f;
            if (prefab == enemyPrefabs[2]) return 100f;
        }
        else if (level == 2)
        {
            if (prefab == enemyPrefabs[0]) return 60f;
            if (prefab == enemyPrefabs[1]) return 100f;
            if (prefab == enemyPrefabs[2]) return 150f;
        }
        else if (level == 3)
        {
            if (prefab == enemyPrefabs[0]) return 100f;
            if (prefab == enemyPrefabs[1]) return 150f;
            if (prefab == enemyPrefabs[2]) return 200f;
        }

        // Default max health value
        return 30f;
    }
}
