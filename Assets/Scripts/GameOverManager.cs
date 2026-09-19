using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalDistanceText;
    public TextMeshProUGUI topDistanceText;
    public ScoreManager scoreManager;

    private bool gameOver = false;
    void Start()
    {
        Time.timeScale = 1f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameOver)
            return;

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
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