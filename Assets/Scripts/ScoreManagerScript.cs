using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI scoreText;

    private float startX;

    void Start()
    {
        startX = player.position.x;
    }
    void Update()
    {
        float distance = player.position.x - startX;
        int meters = Mathf.Max(0, Mathf.FloorToInt(distance));
        scoreText.text = "Distance: " + meters + "m";
    }
    
    public int GetDistance()
    {
        float distance = player.position.x - startX;

        return Mathf.Max(0, Mathf.FloorToInt(distance));
    }
}
