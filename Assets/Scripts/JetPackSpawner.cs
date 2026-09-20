using UnityEngine;
public class JetpackSpawner : MonoBehaviour
{
    [Header("Jetpack")]
    public GameObject jetpackPrefab;
    [Header("Spawning")]
    public float spawnDistance = 30f;
    public float minimumJetpackDistance = 500f;
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
                "JetpackSpawner: No GameObject with the 'Player' tag was found!"
            );
        }
    }
    void Update()
    {
        if (player == null)
            return;
        float spawnX =
            player.position.x + spawnDistance;
        if (spawnX >= lastSpawnX + minimumJetpackDistance)
        {
            SpawnJetpack(spawnX);

            lastSpawnX = spawnX;
        }
    }
    void SpawnJetpack(float spawnX)
    {
        Vector3 spawnPosition = new Vector3(
            spawnX,
            spawnY,
            spawnZ
        );
        Instantiate(
            jetpackPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}