using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    public float moveSpeed = 5f;
    [Header("Left Screen Border")]
    public float leftLimit = 1f;
    [Header("Camera")]
    public float cameraFollowSpeed = 5f;
    private Rigidbody2D rb;
    private Camera mainCamera;
    private float cameraMaxX;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        cameraMaxX = mainCamera.transform.position.x;
    }
    void FixedUpdate()
    {
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
        }
        float cameraLeft = mainCamera.ViewportToWorldPoint(
            new Vector3(0f, 0.5f, 0f)
        ).x;
        float minimumX = cameraLeft + leftLimit;
        if (moveInput < 0 && transform.position.x <= minimumX)
        {
            moveInput = 0f;
        }
        rb.linearVelocity = new Vector2(
            moveInput * moveSpeed,
            rb.linearVelocity.y
        );
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(
                -Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }
    void LateUpdate()
    {
        if (transform.position.x > cameraMaxX)
        {
            cameraMaxX = transform.position.x;
        }
        float newCameraX = Mathf.Lerp(
            mainCamera.transform.position.x,
            cameraMaxX,
            cameraFollowSpeed * Time.deltaTime
        );
        if (newCameraX < mainCamera.transform.position.x)
        {
            newCameraX = mainCamera.transform.position.x;
        }
        mainCamera.transform.position = new Vector3(
            newCameraX,
            mainCamera.transform.position.y,
            mainCamera.transform.position.z
        );
    }
}