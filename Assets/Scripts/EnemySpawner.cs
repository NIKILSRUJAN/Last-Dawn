using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject zombiePrefab;
    public float timeBetweenSpawns = 1.5f; 

    [Header("The Arena Area")]
    // We will drag our invisible trigger box into this slot!
    public BoxCollider2D spawnArea; 

    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnZombieInsideBarrier();
            nextSpawnTime = Time.time + timeBetweenSpawns;
        }
    }

    void SpawnZombieInsideBarrier()
    {
        // Safety check: Make sure we actually assigned the spawn area
        if (spawnArea == null) return;

        // 1. Get the exact mathematical boundaries of our invisible box
        Bounds bounds = spawnArea.bounds;

        // 2. Pick a random X and Y coordinate inside those boundaries
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        // 3. Create the spawn location (Making sure Z is 0!)
        Vector3 spawnLocation = new Vector3(randomX, randomY, 0f);

        // 4. Spawn the zombie!
        Instantiate(zombiePrefab, spawnLocation, Quaternion.identity);
    }
}