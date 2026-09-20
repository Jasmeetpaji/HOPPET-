using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("HEART TOUCHED: " + other.gameObject.name);

        if (!other.CompareTag("Player"))
        {
            Debug.Log("Touched something, but it is NOT the Player.");
            return;
        }

        GameOverManager gameOverManager = 
        other.GetComponentInParent<GameOverManager>();

        if (gameOverManager == null)
        {
            Debug.LogError("Heart found Player, but GameOverManager was not found!");
            return;
        }

        Debug.Log("HEART COLLECTED! Adding 1 life.");
        gameOverManager.AddLife();
        Destroy(gameObject);
    }
}