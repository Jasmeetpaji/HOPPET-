using UnityEngine;
using TMPro;

public class DifficultyManager : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    private float startX;


    [Header("Difficulty")]
    public int difficultyLevel = 1;
    public int maximumDifficultyLevel = 10;
    public float distancePerLevel = 250f;
    private float currentDistance;

    private int previousDifficultyLevel;
    


    [Header("Obstacle Difficulty")]
    public float startingObstacleInterval = 3f;
    public float minimumObstacleInterval = 0.8f;
    public float maximumObstacleSpeed = 8f;
    public float startingObstacleSpeed = 5f;

    [Header("Pickup Difficulty")]
    public float startingHeartWindow = 150f;
    public float minimumHeartWindow = 100f;
    public float startingShieldWindow = 300f;
    public float minimumShieldWindow = 200f;


    [Header("Random Variation")]
    public float obstacleRandomVariation = 0.25f;
    public float speedRandomVariation = 0.5f;
    public bool useRandomVariation = true;

    [Header("Special Events")]
    public bool allowDifficultyBursts = true;
    public float minimumBurstDistance = 500f;
    public float burstDuration = 10f;
    public float burstMultiplier = 1.5f;
    private bool difficultyBurstActive = false;
    private float burstEndTime;
    public float eventCheckInterval = 25f;

    [Header("Calm Periods")]
    public bool allowCalmPeriods = true;
    public float calmDuration = 8f;
    public float calmMultiplier = 0.65f;
    private bool calmPeriodActive = false;
    private float calmEndTime;

    [Header("UI")]
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI distanceText;

    private float currentObstacleInterval;
    private float currentObstacleSpeed;
    private float currentHeartWindow;
    private float currentShieldWindow;
    private float nextEventCheck;
    private float evenCheckInterval = 5f;

    void Start()
    {
        InitializeDifficulty();
    }

    void InitializeDifficulty()
    {
        if (player == null)
        {
            Debug.LogWarning(
                "DifficultyManager: Player is not assigned"
            );

            return;
        }

        startX = player.position.x;
        difficultyLevel = 1;
        previousDifficultyLevel = 1;
        currentDistance = 0f;
        
        currentObstacleInterval =
        startingObstacleInterval;

        currentObstacleSpeed =
        startingObstacleSpeed;

        currentHeartWindow =
        startingHeartWindow;

        currentShieldWindow =
        startingShieldWindow;

        nextEventCheck =
            Time.time + eventCheckInterval;

        UpdateUI();
        Debug.Log(
            "Difficulty Manager Initialized"
        );
    }

    //Update

    void Update()
    {
        if (player == null)
        return;

        UpdateDistance();
        UpdateDifficultyLevel();
        UpdateSpecialEvents();
        UpdateUI();
    }

    //distance

    void UpdateDistance()
    {
        currentDistance =
        player.position.x - startX;

        if (currentDistance < 0f)
        {
            currentDistance = 0f;
        }
    }

    //Difficulty Level

    void UpdateDifficultyLevel()
    {
        float calculatedLevel =
        currentDistance / distancePerLevel;

        int newLevel =
        Mathf.FloorToInt(calculatedLevel) + 1;

        newLevel = 
        Mathf.Clamp(
            newLevel,
            1,
            maximumDifficultyLevel
        );

        difficultyLevel = newLevel;

        if (difficultyLevel != previousDifficultyLevel)
        {
            OnDifficultyLevelChanged();

            previousDifficultyLevel =
            difficultyLevel;
        }

        difficultyLevel = CalculateDifficultyLevel();
    }

    //Calculate Difficulty Values

    void CalculateDifficultyValues()
    {
        float progress =
        (difficultyLevel - 1f) /
        (maximumDifficultyLevel - 1f);
        
        progress = 
        Mathf.Clamp01(progress);

        currentObstacleInterval = 
        Mathf.Lerp(
            startingObstacleSpeed,
            maximumObstacleSpeed,
            progress
        );

        currentHeartWindow =
        Mathf.Lerp(
            startingHeartWindow,
            minimumHeartWindow,
            progress
        );

        currentShieldWindow = 
        Mathf.Lerp(
            startingShieldWindow,
            minimumShieldWindow,
            progress
        );
    }

    //Level change

    void OnDifficultyLevelChanged()
    {
        Debug.Log(
            "Difficulty increased to Level " +
            difficultyLevel
        );

        if (difficultyLevel >= 3)
        {
            Debug.Log(
                "The run is starting to get harder!"
            );
        }

        if (difficultyLevel >= 5)
        {
            Debug.Log(
                "Danger level increased!"
            );
        }

        if (difficultyLevel >= 8)
        {
            Debug.Log(
                "EXTREME DIFFICULTY!!!"
            );
        }
    }

    //Special Events

    void UpdateSpecialEvents()
    {
        if (Time.time < nextEventCheck)
        return;

        nextEventCheck =
        Time.time + eventCheckInterval;

        CheckForSpecialEvent();
    }

    //Event CHECK

    void CheckForSpecialEvent()
    {
        if (difficultyBurstActive)
        return;

        if (calmPeriodActive)
        return;

        if (currentDistance < minimumBurstDistance)

        {
            return;
        }

        float randomChance =
        Random.Range(0f, 1f);

        if (randomChance < 0.15f && allowDifficultyBursts)
        {
            StartDifficultyBurst();

            return;
        }

        if (randomChance > 0.85f &&
            allowCalmPeriods)
        {
            StartCalmPeriod();
        }
    }

    //Difficulty Burst

    void StartDifficultyBurst()
    {
        difficultyBurstActive = true;
        burstEndTime =
        Time.time + burstDuration;

        Debug.Log(
            "DIFFICULTY BURST STARTED!"
        );
    }

    //Calm Period

    void StartCalmPeriod()
    {
        calmPeriodActive = true;

        calmEndTime =
        Time.time + calmDuration;

        Debug.Log(
            "Calm Period Started!"
        );
    } 

    //Special event timer

    void UpdateEventTimers()
    {
        if (difficultyBurstActive)
        {
            if (Time.time >= burstEndTime)
            {
                difficultyBurstActive = false;

                Debug.Log(
                    "Difficulty burst has ended"
                );
            }
        }

        if (calmPeriodActive)
        {
            if (Time.time >= calmEndTime)
            {
                calmPeriodActive = false;

                Debug.Log(
                    "Calm period has ended."
                );
            }
        }
    }

    //Get obstacle interval

    public float GetObstacleInterval()
    {
        UpdateEventTimers();

        float interval =
        currentObstacleInterval;

        if (difficultyBurstActive)
        {
            interval /= burstMultiplier;
        }

        if (calmPeriodActive)
        {
            interval /= calmMultiplier;
        }

        if (useRandomVariation)
        {
            float variation =
            Random.Range(
                -obstacleRandomVariation,
                obstacleRandomVariation
            );

            interval +=
            interval * variation;
        }

        return Mathf.Max(
            0.2f,
            interval
        );
    }

    //Get obstacle speed

    public float GetObstacleSpeed()
    {
        UpdateEventTimers();

        float speed =
        currentObstacleSpeed;

        if (difficultyBurstActive)
        {
            speed *= burstMultiplier;
        }

        if (calmPeriodActive)
        {
            speed *= calmMultiplier;
        }

        if (useRandomVariation)
        {
            float variation =
            Random.Range(
                -speedRandomVariation,
                speedRandomVariation
            );

            speed += variation;
        }

        return Mathf.Max(
            0.1f,
            speed
        );
    }

    //Get heart window

    public float GetHeartWindow()
    {
        return currentHeartWindow;
    }

    //Get Shield Window

    public float GetShieldWindow()
    {
        return currentShieldWindow;
    }

    //Get difficulty level

    public int GetDifficultyLevel()
    {
        return difficultyLevel;
    }

    //Check difficulty burst

    public bool IsDifficultyBurstActive()
    {
        return difficultyBurstActive;
    }

    //Check calm period

    public bool IsCalmPeriodActive()
    {
        return calmPeriodActive;
    }

    //UI

    void UpdateUI()
    {
        if (difficultyText != null)
        {
            difficultyText.text =
            "Difficulty: " +
            difficultyLevel;
        }

        if (distanceText != null)
        {
            distanceText.text =
            "Distance: " +
            Mathf.FloorToInt(
                currentDistance
            ) +
            "m";
        }
    }

    //Debug information

    public string GetDebugInformation()
    {
        string information = "";

        information +=
        "Distance: " +
        Mathf.FloorToInt(
            currentDistance
        ) +
        "m\n";

        information +=
        "Difficulty: " +
        difficultyLevel +
        "\n";

        information +=
        "Obstacle Interval: " +
        currentObstacleInterval +
        "\n";

        information +=
        "Obstacle Speed: " +
        currentObstacleSpeed +
        "\n";

        information +=
        "Heart Window: " +
        currentHeartWindow +
        "\n";

        information +=
        "Shield Window: " +
        currentShieldWindow +
        "\n";

        information +=
        "Burst Active: " +
        difficultyBurstActive +
        "\n";

        information +=
        "Calm Active: " +
        calmPeriodActive;

        return information;
    }

    //Manual Difficulty Controls

    public void IncreaseDifficulty()
    {
        difficultyLevel++;

        difficultyLevel = 
        Mathf.Clamp(
            difficultyLevel,
            1,
            maximumDifficultyLevel
        );

        CalculateDifficultyValues();

        Debug.Log(
            "Difficulty manually increased to " +
            difficultyLevel
        );
    }

    //Decrease difiiculty

    public void DescreaseDifficulty()
    {
        difficultyLevel--;

        difficultyLevel = 
        Mathf.Clamp(
            difficultyLevel,
            1,
            maximumDifficultyLevel
        );

        CalculateDifficultyValues();

        Debug.Log(
            "Difficulty manually decreased to " +
            difficultyLevel
        );
    }

    //Reset

    public void ResetDifficulty()
    {
        difficultyLevel = 1;

        currentDistance =0f;

        currentObstacleInterval = 
        startingObstacleInterval;

        currentObstacleSpeed =
        startingObstacleSpeed;

        currentHeartWindow =
        startingHeartWindow;

        currentShieldWindow =
        startingShieldWindow;

        difficultyBurstActive = false;

        calmPeriodActive = false;

        Debug.Log(
            "Difficulty has been reset."
        );
    }

    //Force Burst

    public void ForceDifficultyBurst()
    {
        StartDifficultyBurst();
    }

    //Force Calm Period

    public void ForceCalmPeriod()
    {
        StartCalmPeriod();
    }

    //Gizmos

    void OnDrawGizmosSelected()
    {
        if (player == null)
        return;

        Gizmos.DrawWireSphere(
            player.position,
            2f
        );
    }

    int CalculateDifficultyLevel()
    {
        int calculatedLevel =
            Mathf.FloorToInt(currentDistance / distancePerLevel) + 1;

        calculatedLevel =
            Mathf.Clamp(
                calculatedLevel,
                1,
                maximumDifficultyLevel
            );

        return calculatedLevel;
    }
}
