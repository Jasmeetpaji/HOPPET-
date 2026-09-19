using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Background")]
    public SpriteRenderer background;

    [Header("World Settings")]
    public float morningDistance = 0f;
    public float dayDistance = 250f;
    public float sunsetDistance = 500f;
    public float nightDistance = 750f;
    public float dawnDistance = 1000f;

    [Header("Colors")]
    public Color morningColor = Color.white;
    public Color dayColor = Color.white;
    public Color sunsetColor = new Color(1f, 0.6f, 0,4);
    public Color nightColor = new Color(0.25f, 0.3f, 0.5f);
    public Color dawnColor = new Color(1f, 0,75f, 0.55f);

    private float startX;

    void Start()
    {
        startX = player.position.x;
    }
    void Update()
    {
        float distance = player.position.x - startX;

        UpdateWorld(distance);
    }
    void UpdateWorld(float distance)
    {
        Color targetColor;

        if (distance < dayDistance)
        {
            targetColor = morningColor;
        }
        else if (distance < sunsetDistance)
        {
            targetColor = dayColor;
        }
        else if (distance < nightDistance)
        {
            targetColor = sunsetColor;
        }
        else if (distance < dawnDistance)
        {
            targetColor = nightColor;
        }
        else
        {
            targetColor = dawnColor;
        }

        if (background != null)
        {
            background.color = targetColor;
        }
    }
}
