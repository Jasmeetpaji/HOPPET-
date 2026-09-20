using UnityEngine;
public class SpeedBoostSpawner : MonoBehaviour
{
    [Header("Speed Boost")]
    public GameObject speedBoostPrefab;
    [Header("Spawning")]
    public float spawnDistance = 25f;
    public float minimumBoostDistance = 100f;
    [Header("Position")]
    public float spawnY = -1f;
    public float spawnZ = 0f;
    private Transform player;
    private float lastSpawnX = -100f;
    void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError(
                "SpeedBoostSpawner: No GameObject with the 'Player' tag was found!"
            );
        }
    }
    void Update()
    {
        if (player == null)
            return;
        float spawnX =
            player.position.x + spawnDistance;
        if (spawnX >= lastSpawnX + minimumBoostDistance)
        {
            SpawnSpeedBoost(spawnX);
            lastSpawnX = spawnX;
        }
    }
    void SpawnSpeedBoost(float spawnX)
    {
        Vector3 spawnPosition = new Vector3(
            spawnX,
            spawnY,
            spawnZ
        );
        Instantiate(
            speedBoostPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}