using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalDistanceText;
    public TextMeshProUGUI topDistanceText;
    public ScoreManager scoreManager;
    [Header("Lives")]
    public TextMeshProUGUI livesText;
    public int maxLives = 3;
    [Header("Shield")]
    public GameObject shieldEffect;
    public float shieldDuration = 5f;
    [Header("Player")]
    public PlayerMovement playerMovement;
    [Header("Game Over Sound")]
    public AudioSource gameOverAudioSource;
    [Header("Background Music")]
    public AudioSource backgroundMusicSource;
    private int currentLives;
    private bool gameOver = false;
    private bool shieldActive = false;
    private float shieldEndTime;
    private float nextHitTime = 0f;
    public float hitCooldown = 1f;
    void Start()
    {
        Time.timeScale = 1f;
        currentLives = maxLives;
        shieldActive = false;
        if (playerMovement == null)
            playerMovement = FindFirstObjectByType<PlayerMovement>();
        if (playerMovement == null)
            Debug.LogError("GameOverManager: PlayerMovement was not found!");
        if (shieldEffect != null)
            shieldEffect.SetActive(false);
        UpdateLivesUI();
    }
    void Update()
    {
        if (shieldActive && Time.time >= shieldEndTime)
            DeactivateShield();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameOver)
            return;
        if (!collision.gameObject.CompareTag("Obstacle"))
            return;
        if (shieldActive)
        {
            Debug.Log("Shield protected the player!");
            return;
        }
        if (playerMovement != null &&
            playerMovement.IsJetpackActive())
        {
            Debug.Log("Jetpack protected the player!");
            return;
        }
        if (Time.time < nextHitTime)
            return;
        nextHitTime = Time.time + hitCooldown;
        LoseLife();
    }
    void LoseLife()
    {
        currentLives--;
        UpdateLivesUI();
        Debug.Log("Player hit! Lives remaining: " + currentLives);
        if (currentLives <= 0)
            GameOver();
    }
    public void AddLife()
    {
        currentLives++;
        if (currentLives > maxLives)
            currentLives = maxLives;
        UpdateLivesUI();
        Debug.Log("Extra life added! Lives: " + currentLives);
    }
    void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = "Lives: " + currentLives;
    }
    public void ActivateShield()
    {
        shieldActive = true;
        shieldEndTime = Time.time + shieldDuration;
        if (shieldEffect != null)
            shieldEffect.SetActive(true);
        Debug.Log("Shield activated!");
    }
    void DeactivateShield()
    {
        shieldActive = false;
        if (shieldEffect != null)
            shieldEffect.SetActive(false);
        Debug.Log("Shield expired!");
    }
    void GameOver()
    {
        gameOver = true;
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.Stop();
        }
        if (gameOverAudioSource != null)
        {
            gameOverAudioSource.Play();
        }
        int finalDistance = scoreManager.GetDistance();
        int topDistance =
            PlayerPrefs.GetInt("TopDistance", 0);
        if (finalDistance > topDistance)
        {
            topDistance = finalDistance;
            PlayerPrefs.SetInt(
                "TopDistance",
                topDistance
            );
            PlayerPrefs.Save();
        }
        if (finalDistanceText != null)
            finalDistanceText.text =
                "Distance: " + finalDistance + "m";
        if (topDistanceText != null)
            topDistanceText.text =
                "Top Distance: " + topDistance + "m";
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
    public void MaineMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MaineMenu");
    }
}