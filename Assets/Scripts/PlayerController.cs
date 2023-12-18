using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class PlayerController : MonoBehaviour
{
    // Keyboard inputs
    private float horizontalInput;
    private float verticalInput;
    private float ascentInput;

    public float mouseSensitivity = 100.0f;

    // Player boundary
    private float speed = 25.0f; // Player speed
    private Vector3 boundarySize = new Vector3(60, 50, 50);
    public float ascentSpeed = 20.0f;

    // Leaning
    public float leanAngle = 15.0f; // Max angle for leaning
    public float leanSpeed = 5.0f;

    // Projectile object reference for instantiation
    public GameObject projectilePrefab;
    public Transform shootingPoint; // Shooting point reference
    private Rigidbody rigBod;

    // Player life and bullet
    int reload = 10;
    int bulletCount = 10;
    int maxBullets = 10;
    public TextMeshProUGUI bulletCounterText; // Assuming you have a UI Text element for displaying bullet count

    //Referring to SpawnManager script
    private SpawnManager spawnManager;
    private int lastKillCountCheck = 0;

    private void Start()
    {
        rigBod = GetComponent<Rigidbody>();
        maxBullets = 10;
        bulletCount = maxBullets;
        spawnManager = FindObjectOfType<SpawnManager>(); // Find the SpawnManager in the scene
        
    }
    void FixedUpdate()
    {
        UpdateBulletCounterUI();
    }

     void Update()
     {
        HandleMouseInput();
        HandleLeaningInput();
        CheckBounds();
        HandleMovementInput();
        //sssssssssssCheckForNewKills();
     }

    private void HandleMouseInput()
    {
        float horizontalRotation = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        transform.Rotate(Vector3.up, horizontalRotation);

        if (Input.GetMouseButtonDown(1))
        {
            ReloadBullets();
            //CheckForNewKills();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (bulletCount > 0)
            {
                Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
                bulletCount--;
                UpdateBulletCounterUI();
            }
            else
            {
                UnityEngine.Debug.Log("Out of bullets! Right Click Mouse Button.");
            }
        }
    }
    /*
    private void CheckForNewKills()
    {
        // Only proceed if spawnManager is found
        if (spawnManager != null)
        {
            int currentKillCount = spawnManager.killCount;

            // Check if 3 or more enemies have been killed since the last check
            if (currentKillCount - lastKillCountCheck >= 3)
            {
                lastKillCountCheck = currentKillCount;
                ReloadBullets(); // Reset bullet count every time 3 enemies are killed
            }
        }
    }
    */

    private void ReloadBullets()
    {
        if (bulletCount <= 0)
        {
            bulletCount = reload;
            UpdateBulletCounterUI();
        }
    }
    
    private void UpdateBulletCounterUI()
    {
        if (bulletCounterText != null)
        {
            bulletCounterText.text = "BULLETS: " + bulletCount.ToString();
        }
    }

    private void HandleLeaningInput()
    {
        float currentLean = 0;
        if (Input.GetKey(KeyCode.Q))
        {
            currentLean = leanSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            currentLean = -leanSpeed * Time.deltaTime;
        }

        // Lean rotation around z-axis
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z + currentLean);
    }

    private void CheckBounds()
    {
        // Get the player's current position
        Vector3 currentPosition = transform.position;

        // Clamp the position within the specified boundaries
        currentPosition.x = Mathf.Clamp(currentPosition.x, -boundarySize.x, boundarySize.x);
        currentPosition.y = Mathf.Clamp(currentPosition.y, -boundarySize.y, boundarySize.y);
        currentPosition.z = Mathf.Clamp(currentPosition.z, -boundarySize.z, boundarySize.z);

        // Apply the clamped position back to the player
        transform.position = currentPosition;
    }

    private void HandleMovementInput()
    {
        // Get input from the player
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        ascentInput = 0;

        if (Input.GetKey(KeyCode.Space)) // Ascend
        {
            ascentInput = ascentSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift)) // Descend
        {
            ascentInput = -ascentSpeed;
        }

        // Calculate movement direction
        Vector3 moveDirection = (transform.forward * verticalInput + transform.right * horizontalInput).normalized;
        Vector3 ascentDirection = new Vector3(0, ascentInput, 0);

        // Increase speed if the "W" key is pressed
        if (Input.GetKey(KeyCode.W))
        {
            speed = 20.0f; // Adjust the speed
        }
        else
        {
            speed = 10.0f; // Reset speed to default
        }

        // Move the player using Rigidbody
        rigBod.velocity = moveDirection * speed + ascentDirection;
    }
}
