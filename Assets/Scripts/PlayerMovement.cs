using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    public float moveSpeed = 5f;
    [Header("Speed Boost")]
    public float speedBoostMultiplier = 2f;
    private float normalMoveSpeed;
    private float speedBoostEndTime;
    private bool speedBoostActive = false;
    [Header("Jetpack")]
    public float jetpackForwardSpeed = 10f;
    public float jetpackFlightHeight = 3f;
    public float jetpackTakeoffTime = 1f;
    public float jetpackDuration = 10f;
    public float jetpackLandingTime = 2f;
    private bool jetpackActive = false;
    private float jetpackTimer = 0f;
    private float jetpackStartY;
    private float jetpackTargetY;
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
        normalMoveSpeed = moveSpeed;
    }
    void Update()
    {
        if (speedBoostActive &&
            Time.time >= speedBoostEndTime)
        {
            DeactivateSpeedBoost();
        }
        if (jetpackActive)
        {
            jetpackTimer += Time.deltaTime;
            if (jetpackTimer >= jetpackDuration)
            {
                EndJetpack();
            }
        }
    }
    void FixedUpdate()
    {
        if (jetpackActive)
        {
            HandleJetpackMovement();
            return;
        }
        HandleNormalMovement();
    }
    void HandleNormalMovement()
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
        float cameraLeft =
            mainCamera.ViewportToWorldPoint(
                new Vector3(0f, 0.5f, 0f)
            ).x;
        float minimumX =
            cameraLeft + leftLimit;
        if (moveInput < 0 &&
            transform.position.x <= minimumX)
        {
            moveInput = 0f;
        }
        rb.linearVelocity = new Vector2(
            moveInput * moveSpeed,
            rb.linearVelocity.y
        );
        if (moveInput > 0)
        {
            transform.localScale =
                new Vector3(
                    Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z
                );
        }
        else if (moveInput < 0)
        {
            transform.localScale =
                new Vector3(
                    -Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z
                );
        }
    }
    void HandleJetpackMovement()
    {
        float targetY = jetpackTargetY;
        if (jetpackTimer < jetpackTakeoffTime)
        {
            float progress =
                jetpackTimer / jetpackTakeoffTime;
            float newY =
                Mathf.Lerp(
                    jetpackStartY,
                    targetY,
                    progress
                );
            float verticalSpeed =
                (newY - transform.position.y) /
                Time.fixedDeltaTime;
            rb.linearVelocity =
                new Vector2(
                    jetpackForwardSpeed,
                    verticalSpeed
                );
        }
        else if (
            jetpackTimer <
            jetpackDuration - jetpackLandingTime
        )
        {
            float difference =
                targetY - transform.position.y;
            float verticalSpeed =
                difference * 8f;
            rb.linearVelocity =
                new Vector2(
                    jetpackForwardSpeed,
                    verticalSpeed
                );
        }
        else
        {
            float landingProgress =
                (
                    jetpackTimer -
                    (jetpackDuration - jetpackLandingTime)
                )
                / jetpackLandingTime;
            float groundY =
                jetpackStartY;
            float newY =
                Mathf.Lerp(
                    targetY,
                    groundY,
                    landingProgress
                );
            float verticalSpeed =
                (newY - transform.position.y) /
                Time.fixedDeltaTime;
            rb.linearVelocity =
                new Vector2(
                    Mathf.Lerp(
                        jetpackForwardSpeed,
                        moveSpeed,
                        landingProgress
                    ),
                    verticalSpeed
                );
        }
        transform.localScale =
            new Vector3(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
    }
    public void ActivateJetpack(float duration)
    {
        jetpackActive = true;
        jetpackTimer = 0f;
        jetpackDuration = duration;
        jetpackStartY =
            transform.position.y;
        jetpackTargetY =
            jetpackStartY +
            jetpackFlightHeight;
        rb.gravityScale = 0f;
        rb.linearVelocity =
            new Vector2(
                jetpackForwardSpeed,
                0f
            );
        Debug.Log(
            "JETPACK ACTIVATED!"
        );
    }
    void EndJetpack()
    {
        jetpackActive = false;
        Vector3 position =
            transform.position;
        position.y =
            jetpackStartY;
        transform.position =
            position;
        rb.gravityScale = 1f;
        rb.linearVelocity =
            new Vector2(
                moveSpeed,
                0f
            );
        Debug.Log(
            "JETPACK ENDED!"
        );
    }
    public bool IsJetpackActive()
    {
        return jetpackActive;
    }
    public void ActivateSpeedBoost(
        float multiplier,
        float duration
    )
    {
        normalMoveSpeed = 5f;
        moveSpeed =
            normalMoveSpeed * multiplier;
        speedBoostEndTime =
            Time.time + duration;
        speedBoostActive = true;
        Debug.Log(
            "SUPER SPEED ACTIVATED! " +
            "Speed: " +
            moveSpeed +
            " for " +
            duration +
            " seconds."
        );
    }
    void DeactivateSpeedBoost()
    {
        moveSpeed =
            normalMoveSpeed;
        speedBoostActive = false;
        Debug.Log(
            "SUPER SPEED ENDED!"
        );
    }
    void LateUpdate()
    {
        if (transform.position.x >
            cameraMaxX)
        {
            cameraMaxX =
                transform.position.x;
        }
        float newCameraX =
            Mathf.Lerp(
                mainCamera.transform.position.x,
                cameraMaxX,
                cameraFollowSpeed *
                Time.deltaTime
            );
        if (newCameraX <
            mainCamera.transform.position.x)
        {
            newCameraX =
                mainCamera.transform.position.x;
        }
        mainCamera.transform.position =
            new Vector3(
                newCameraX,
                mainCamera.transform.position.y,
                mainCamera.transform.position.z
            );
    }
}