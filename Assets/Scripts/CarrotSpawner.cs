using UnityEngine;
public class CarrotSpawner : MonoBehaviour
{
    [Header("Carrot")]
    public GameObject carrotPrefab;
    [Header("Spawning")]
    public float spawnDistance = 18f;
    public float minimumCarrotDistance = 10f;
    [Header("Position")]
    public float groundY = -1f;
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
            Debug.LogError("CarrotSpawner: No GameObject with the 'Player' tag was found!");
        }
    }
    void Update()
    {
        if (player == null)
            return;
        float spawnX = player.position.x + spawnDistance;
        if (spawnX >= lastSpawnX + minimumCarrotDistance)
        {
            SpawnCarrot(spawnX);
            lastSpawnX = spawnX;
        }
    }
    void SpawnCarrot(float spawnX)
    {
        Vector3 spawnPosition = new Vector3(
            spawnX,
            groundY,
            spawnZ
        );
        Instantiate(
            carrotPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}