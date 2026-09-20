using UnityEngine;
public class MysteryBoxSpawner : MonoBehaviour
{
    [Header("Mystery Box")]
    public GameObject mysteryBoxPrefab;
    [Header("Spawning")]
    public float spawnDistance = 20f;
    public float minimumBoxDistance = 25f;
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
            Debug.LogError("MysteryBoxSpawner: No GameObject with the 'Player' tag was found!");
        }
    }
    void Update()
    {
        if (player == null)
            return;
        float spawnX = player.position.x + spawnDistance;
        if (spawnX >= lastSpawnX + minimumBoxDistance)
        {
            SpawnMysteryBox(spawnX);
            lastSpawnX = spawnX;
        }
    }
    void SpawnMysteryBox(float spawnX)
    {
        Vector3 spawnPosition = new Vector3(
            spawnX,
            groundY,
            spawnZ
        );
        Instantiate(
            mysteryBoxPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}