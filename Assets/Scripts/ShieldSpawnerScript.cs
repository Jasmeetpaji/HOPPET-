using UnityEngine;

public class ShieldSpawner : MonoBehaviour
{
    [Header("Shield")]
    public GameObject shieldPrefab;

    [Header("Player")]
    public Transform player;

    [Header("Random Spawn Windows")]
    public float windowSize = 150f;
    public float gapBetweenWindows = 10f;

    [Header("Spawn Position")]
    public float spawnDistance = 15f;
    public float spawnY = -1.5f;
    private float startX;
    private float nextWindowStart;
    private float nextSpawnDistance;

    void Start()
    {
        if (player == null)
            return;

        startX = player.position.x;
        nextWindowStart = 1f;
        ChooseRandomSpawnDistance();
    }

    void Update()
    {
        if (player == null)
            return;

        float distance =
            player.position.x - startX;

        if (distance >= nextSpawnDistance)
        {
            SpawnShield();
            
            nextWindowStart +=
                windowSize + gapBetweenWindows;

            ChooseRandomSpawnDistance();
        }
    }

    void ChooseRandomSpawnDistance()
    {
        float windowEnd =
            nextWindowStart + windowSize;
            nextSpawnDistance =
            Random.Range(
                nextWindowStart,
                windowEnd
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