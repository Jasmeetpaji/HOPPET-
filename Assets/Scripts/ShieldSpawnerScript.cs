using UnityEngine;

public class ShieldSpawner : MonoBehaviour
{
    [Header("Shield")]
    public GameObject shieldPrefab;
    [Header("Player")]
    public Transform player;
    [Header("Spawning")]
    public float spawnDistance = 15f;
    public float minimumSpawnTime = 8f;
    public float maximumSpawnTime = 15f;
    [Header("Height")]
    public float spawnY = -1.5f;
    private float nextSpawnTime;
    void Start()
    {
        SetNextSpawnTime();
    }

    void Update()
    {
        if (player == null)
            return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnShield();

            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time +
            Random.Range(
                minimumSpawnTime,
                maximumSpawnTime
            );
    }

    void SpawnShield()
    {
        float spawnX =
            player.position.x + spawnDistance;

        Vector3 spawnPosition =
            new Vector3(
                spawnX,
                spawnY,
                0f
            );

        Instantiate(
            shieldPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}
