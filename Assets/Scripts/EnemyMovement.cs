using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public delegate void AllDied();
    public static event AllDied OnAllDied;
    
    public int rows = 5;
    public int columns = 5;
    public float moveSpeed = 2f; 
    public float dropSpeed = 0.25f;
    public float speedIncrease = 0.25f;
    
    public float boundsXMin = -5f; // Minimum x-axis bound
    public float boundsXMax = 5f;  // Maximum x-axis bound

    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public Transform gridRoot;

    private int direction = 1; // 1 for moving to the right, -1 for moving to the left

    private void Start()
    {
        Enemy.OnEnemyDied += EnemyOnOnEnemyDied;
        LoadGrid();
    }
    
    private void OnDestroy()
    {
        Enemy.OnEnemyDied -= EnemyOnOnEnemyDied;
    }
    
    void EnemyOnOnEnemyDied(int points)
    {
        moveSpeed += speedIncrease; // increase grid speed when an enemy dies
        int numOfChildren = gridRoot.childCount;
        // Debug.Log(numOfChildren);
        if (numOfChildren == 1)
        {
            OnAllDied.Invoke();
        }
    }

    void FixedUpdate()
    {
        MoveGrid();
    }

    private void LoadGrid()
    {
        for (int r = 0; r > -rows; r--)
        {
            for (int c = 0; c < columns; c++)
            {
                // create spawn pos from row/column
                Vector3 spawnPos = new Vector3(c * 1.5f, r, 0f); // * 1.5f for spacing
                if (r == -rows + 1)
                {
                    GameObject enemyInstance = Instantiate(enemyPrefab1, gridRoot.TransformPoint(spawnPos), Quaternion.identity, gridRoot);
                }
                else if (r == -rows + 2)
                {
                    GameObject enemyInstance = Instantiate(enemyPrefab2, gridRoot.TransformPoint(spawnPos), Quaternion.identity, gridRoot);
                }
                else if (r <= 0)
                {
                    GameObject enemyInstance = Instantiate(enemyPrefab3, gridRoot.TransformPoint(spawnPos), Quaternion.identity, gridRoot);
                }
            }
        }
    }

    void MoveGrid()
    {
        // Calculate the movement vector
        Vector3 movement = new Vector3(direction * moveSpeed * Time.deltaTime, 0f, 0f);
        transform.Translate(movement);

        // Check if the grid reached the x-axis bounds
        if (CheckBounds())
        {
            // Change direction when reaching bounds
            direction *= -1;

            // move grid down
            gridRoot.position = new Vector3(gridRoot.position.x, gridRoot.position.y - dropSpeed, 0f);
        }
    }

    bool CheckBounds()
    {
        float gridXPos = gridRoot.position.x;
        if (gridXPos < boundsXMin + 1 || gridXPos > boundsXMax - columns - 2f)
        {
            return true;
        }

        return false;

    }
}
