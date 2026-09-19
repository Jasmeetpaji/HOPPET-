using UnityEngine;
public class RockSpawner : MonoBehaviour
{
    [Header("Obstacles")]
    public GameObject rockPrefab;
    public GameObject logPrefab;
    [Header("Spawning")]
    public float spawnDistance = 15f;
    public float minimumObstacleDistance = 8f;
    [Header("Obstacle Position")]
    public float groundY = -2f;
    public float spawnZ = 0f;
    private Transform player;
    private float lastSpawnX = -100f;
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("RockSpawner: No GameObject with the 'Player' tag was found!");
        }
    }
    void Update()
    {
        if (player == null)
            return;
        float spawnX = player.position.x + spawnDistance;
        if (spawnX >= lastSpawnX + minimumObstacleDistance)
        {
            SpawnObstacle(spawnX);
            lastSpawnX = spawnX;
        }
    }
    void SpawnObstacle(float spawnX)
    {
        Vector3 spawnPosition = new Vector3(
            spawnX,
            groundY,
            spawnZ
        );
        GameObject obstacleToSpawn;
        if (Random.Range(0, 2) == 0)
        {
            obstacleToSpawn = rockPrefab;
        }
        else
        {
            obstacleToSpawn = logPrefab;
        }
        Instantiate(
            obstacleToSpawn,
            spawnPosition,
            Quaternion.identity
        );
    }
}