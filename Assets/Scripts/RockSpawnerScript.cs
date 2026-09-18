using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [Header("Rock")]
    public GameObject rockPrefab;

    [Header("Spawning")]
    public float spawnDistance = 15f;
    public float spawnInterval = 3f;
    public float groundY = -2f;

    private Transform player;
    private float nextSpawnTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        nextSpawnTime = Time.time + 2f;
    }

    void Update()
    {
        if (player == null)
            return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnRock();

            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnRock()
    {
        float spawnX = player.position.x + spawnDistance;

        Vector3 spawnPosition = new Vector3(
            spawnX,
            groundY,
            0f
        );

        Instantiate(
            rockPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}
