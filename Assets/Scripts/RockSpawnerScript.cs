using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [Header("Rock")]
    public GameObject rockPrefab;

    [Header("Spawning")]
    public float spawnDistance = 15f;
    public float spawnInterval = 3f;
    public float groundY = -2f;

    [Header("Difficulty")]
    public DifficultyManager difficultyManager;

    private Transform player;
    private float nextSpawnTime;

    void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject == null)
        {
            Debug.LogError(
                "RockSpawner: No GameObject with the 'Player' tag was found!"
            );

            return;
        }

        player = playerObject.transform;

        nextSpawnTime = Time.time + 2f;
    }

    void Update()
    {
        if (player == null)
            return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnRock();

            float currentInterval = spawnInterval;

            if (difficultyManager != null)
            {
                currentInterval =
                    difficultyManager.GetObstacleInterval();
            }

            nextSpawnTime =
                Time.time + currentInterval;
        }
    }

    void SpawnRock()
    {
        float spawnX =
            player.position.x + spawnDistance;

        Vector3 spawnPosition =
            new Vector3(
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