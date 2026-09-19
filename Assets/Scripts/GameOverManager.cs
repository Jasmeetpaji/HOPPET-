using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalDistanceText;
    public TextMeshProUGUI topDistanceText;

    [Header("Score")]
    public ScoreManager scoreManager;

    [Header("Lives")]
    public TextMeshProUGUI livesText;
    public int maxLives = 3;

    [Header("Shield")]
    public GameObject shieldEffect;
    public float shieldDuration = 5f;

    private int currentLives;
    private bool gameOver = false;

    private bool shieldActive = false;
    private float shieldEndTime;

    // Small protection so one obstacle cannot remove
    // multiple lives from repeated collisions.
    private float nextHitTime = 0f;
    public float hitCooldown = 1f;
    void Start()
    {
        Time.timeScale = 1f;

        currentLives = maxLives;
        
        shieldActive = false;

        if (shieldEffect != null)
        {
            shieldEffect.SetActive(false);
        }

        UpdateLivesUI();
    }

    void Update()
    {
        if (shieldActive && Time.time >= shieldEndTime)
        {
            DeactivateShield();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameOver)
            return;

        if (!collision.gameObject.CompareTag("Obstacle"))
            return;
        
        // Shield protects the player from obstacles.
        if (shieldActive)
            return;

        if (Time.time < nextHitTime)
            return;

        nextHitTime = Time.time + hitCooldown;

        LoseLife();
    }
    void LoseLife()
    {
        currentLives--;

        UpdateLivesUI();

        if (currentLives <= 0)
        {
            GameOver();
        }
    }
    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives;
        }
    }

    public void ActivateShield()
    {
        shieldActive = true;
        shieldEndTime = Time.time + shieldDuration;
        if (shieldEffect != null)
        {
            shieldEffect.SetActive(true);
        }
    }

    void DeactivateShield()
    {
        shieldActive = false;

        if (shieldEffect != null)
        {
            shieldEffect.SetActive(false);
        }
    }
    
    void GameOver()
    {
        gameOver = true;

        int finalDistance = scoreManager.GetDistance();
        int topDistance = PlayerPrefs.GetInt("TopDistance", 0);
        
        if (finalDistance > topDistance)
        {
            topDistance = finalDistance;
            PlayerPrefs.SetInt("TopDistance", topDistance);
            PlayerPrefs.Save();
        }

        finalDistanceText.text = "Distance: " + finalDistance + "m";
        topDistanceText.text = "Top Distance: " + topDistance + "m";
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
}
