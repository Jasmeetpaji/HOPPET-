using UnityEngine;
public class MysteryBox : MonoBehaviour
{
    private CarrotRewardPopup rewardPopup;
    void Start()
    {
        rewardPopup = FindFirstObjectByType<CarrotRewardPopup>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int carrotReward = Random.Range(20, 101);
            if (CarrotManager.instance != null)
            {
                for (int i = 0; i < carrotReward; i++)
                {
                    CarrotManager.instance.AddCarrot();
                }
            }
            if (rewardPopup != null)
            {
                rewardPopup.ShowReward(carrotReward);
            }
            Debug.Log("Mystery Box gave " + carrotReward + " carrots!");
            Destroy(gameObject);
        }
    }
}