using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] enemySpawnPoints;
    private string[] sceneNames = { "Level1", "Level2", "Level3", "Lose", "Victory" };

    private float spawnInterval = 3.0f;
    private int spawnCount;
    public int spawnMax;
    public int killCount;
    public TextMeshProUGUI killCounterText;
    private int movementSpeedIncrease = 5;

    void Start()
    {
        //Find scene name
        CheckCurrentSceneName();

        //Start spawning enemies repeatedly after an initial delay
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
    }

    void Update()
    {
        VictoryCheck(); //
        SecondPhase(); // Call the SecondPhase method in the Update loop
    }

    //find current scene;s name and if its different reset spawnMax
    void CheckCurrentSceneName()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == sceneNames[0]) // Level1
        {
            spawnMax = 5;
        }
        else if (currentSceneName == sceneNames[1]) // Level2
        {
            spawnMax = 10;
        }
        else if (currentSceneName == sceneNames[2]) // Level3
        {
            spawnMax = int.MaxValue; // Infinite spawn on Level3
        }
    }

    void SpawnEnemy()
    {
        if (spawnCount < spawnMax)
        {
            int enemyIndex = UnityEngine.Random.Range(0, enemyPrefabs.Length);
            GameObject selectedEnemyPrefab = enemyPrefabs[enemyIndex];

            int spawnPointIndex = UnityEngine.Random.Range(0, enemySpawnPoints.Length);
            Vector3 spawnPos = enemySpawnPoints[spawnPointIndex].transform.position;

            Instantiate(selectedEnemyPrefab, spawnPos, Quaternion.identity);
            spawnCount++;
            UnityEngine.Debug.Log("Current spawn count: " + spawnCount);
        }
    }

    public void EnemyKilled()
    {
        killCount++;
        UpdateKillCounterUI();
    }

    void UpdateKillCounterUI()
    {
        if (killCounterText != null)
        {
            killCounterText.text = "Kills: " + killCount.ToString();
        }
    }

    public int SecondPhase()
    {
        if (killCount == 10)
        {
            movementSpeedIncrease = movementSpeedIncrease + 10;
        }
        return movementSpeedIncrease;
    }

    void VictoryCheck()
    {
        if (killCount >= spawnMax)
        {
            UnityEngine.Debug.Log("Victory");
            killCount = 0;
            killCounterText.text = "Kills: " + killCount;
            SceneManager.LoadScene("Victory"); // Load the Victory scene when enough enemies have been killed
        }
        else
        {
            // Reset the text when the scene changes
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName != sceneNames[0] && currentSceneName != sceneNames[1] && currentSceneName != sceneNames[2])
            {
                killCount = 0;
                killCounterText.text = "Kills: " + killCount;
            }
        }
    }
}
