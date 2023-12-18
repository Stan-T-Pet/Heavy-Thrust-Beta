using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int speed = 5;
    public float leftBound = -20f;
    public float rightBound = 20f;
    private SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    void Update()
    {
        MoveEnemy();

        // Access SecondPhase as a method
        speed = spawnManager.SecondPhase();
    }

    void MoveEnemy()
    {
        // Move the enemy from left to right
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Check if the enemy is out of bounds, and reverse its direction
        if (transform.position.x >= rightBound || transform.position.x <= leftBound)
        {
            speed = -speed;
        }
    }
}
