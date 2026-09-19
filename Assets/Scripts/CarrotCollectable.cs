using UnityEngine;
public class CarrotCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (CarrotManager.instance != null)
            {
                CarrotManager.instance.AddCarrot();
            }

            Destroy(gameObject);
        }
    }
}