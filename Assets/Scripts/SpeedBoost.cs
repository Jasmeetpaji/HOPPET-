using UnityEngine;
public class SpeedBoostPickup : MonoBehaviour
{
    [Header("Speed Boost")]
    public float boostMultiplier = 2f;
    public float boostDuration = 10f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        PlayerMovement playerMovement =
            other.GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            playerMovement =
                other.GetComponentInParent<PlayerMovement>();
        }
        if (playerMovement == null)
        {
            Debug.LogError(
                "Speed Boost found Player, but PlayerMovement was not found!"
            );
            return;
        }
        playerMovement.ActivateSpeedBoost(
            boostMultiplier,
            boostDuration
        );
        Debug.Log("SPEED BOOST ACTIVATED!");
        Destroy(gameObject);
    }
}