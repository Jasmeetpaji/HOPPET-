using UnityEngine;
public class JetpackPickup : MonoBehaviour
{
    [Header("Jetpack")]
    public float flightDuration = 10f;
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
                "Jetpack found Player, but PlayerMovement was not found!"
            );
            return;
        }
        playerMovement.ActivateJetpack(flightDuration);
        Debug.Log("JETPACK ACTIVATED!");
        Destroy(gameObject);
    }
}