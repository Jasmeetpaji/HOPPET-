using UnityEngine;
public class ShieldPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        GameOverManager gameOverManager =
            other.GetComponent<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.ActivateShield();
        }
        Destroy(gameObject);
    }
}
