using UnityEngine;
public class GroundLoop : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void LateUpdate()
    {
        Camera cam = Camera.main;
        if (cam == null)
            return;
        float cameraLeft = cam.ViewportToWorldPoint(
            new Vector3(0f, 0f, 0f)
        ).x;
        if (spriteRenderer.bounds.max.x <= cameraLeft)
        {
            RecycleTile();
        }
    }
    void RecycleTile()
    {
        GameObject[] grounds =
            GameObject.FindGameObjectsWithTag("Ground");
        GameObject otherGround = null;
        foreach (GameObject ground in grounds)
        {
            if (ground != gameObject)
            {
                otherGround = ground;
                break;
            }
        }
        if (otherGround == null)
            return;
        SpriteRenderer otherRenderer =
            otherGround.GetComponent<SpriteRenderer>();
        float width = spriteRenderer.bounds.size.x;
        float newX =
            otherRenderer.bounds.max.x + (width / 2f);
        transform.position = new Vector3(
            newX,
            transform.position.y,
            transform.position.z
        );
    }
}