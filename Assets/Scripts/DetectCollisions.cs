using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DetectCollisions : MonoBehaviour
{
    private int lifeCounter = 3;
    public TextMeshProUGUI playerLifeText;

    void FixedUpdate()
    {
        UpdatePlayerLifeUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("EnemyBullet"))
            {
                HandleEnemyBulletCollision();
            }
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.CompareTag("PlayerBullet"))
            {
                HandlePlayerBulletCollision();
            }
        }
    }

    private void HandleEnemyBulletCollision()
    {
        lifeCounter--;

        UpdatePlayerLifeUI();

        if (lifeCounter <= 0)
        {
            UnityEngine.Debug.Log("Lose");
            SceneManager.LoadScene("Lose");
        }
    }

    private void HandlePlayerBulletCollision()
    {
        FindObjectOfType<SpawnManager>().EnemyKilled();
        Destroy(gameObject);
    }

    void UpdatePlayerLifeUI()
    {
        if (playerLifeText != null)
        {
            playerLifeText.text = "LIFE: " + Mathf.Max(lifeCounter, 0).ToString();
        }
    }
}
