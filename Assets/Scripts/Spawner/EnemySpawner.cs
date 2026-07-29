using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Spawn")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float initialDelay = 1f;
    [SerializeField] private float spawnInterval = 2f;

    private Coroutine spawnRoutine;

    private void Start()
    {
        if (!CanSpawn())
        {
            enabled = false;
            return;
        }

        spawnRoutine = StartCoroutine(SpawnEnemies());
    }

    private bool CanSpawn()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("EnemySpawner requires an enemy prefab", this);

            return false;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("EnemySpawner requires at least one spawn point", this);
            return false;
        }

        return true;
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform selectedSpawnPoint = spawnPoints[randomIndex];

        Instantiate(enemyPrefab, selectedSpawnPoint.position, selectedSpawnPoint.rotation);
    }

    private void OnDisable()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }
}
