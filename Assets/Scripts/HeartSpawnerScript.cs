using UnityEngine;

public class HeartSpawner : MonoBehaviour
{
    [Header("Heart")]
    public GameObject heartPrefab;

    [Header("Player")]
    public Transform player;

    [Header("Spawning")]
    public float spawnDistance = 15f;
    public float spawnEveryMeters = 200f;

    [Header("Height")]
    public float spawnY = -1.5f;

    private float startX;
    private float nextSpawnDistance;

    void Start()
    {
        if (player == null)
        return;

        startX = player.position.x;

        nextSpawnDistance = spawnEveryMeters;
    }
    void Update()
    {
        if (player == null)
        return;

        float distance = 
        player.position.x - startX;

        if (distance >= nextSpawnDistance)
        {
            SpawnHeart();
            nextSpawnDistance += spawnEveryMeters;
        }
    }

    void SpawnHeart()
    {
        float spawnX =
        player.position.x + spawnDistance;
        Vector3 spawnPosition = new Vector3(
            spawnX,
            spawnY,
            0f
        );

        Instantiate(
            heartPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}
