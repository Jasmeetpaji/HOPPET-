using UnityEngine;
public class RabbitHop : MonoBehaviour
{
    public float hopHeight = 0.15f;
    public float hopSpeed = 8f;
    private float startY;
    void Start()
    {
        startY = transform.localPosition.y;
    }
    void Update()
    {
        float hop = Mathf.Abs(Mathf.Sin(Time.time * hopSpeed)) * hopHeight;
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            startY + hop,
            transform.localPosition.z
        );
    }
}