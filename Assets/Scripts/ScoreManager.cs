using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiScoreText;
    private int scoreValue;
    private int hiScoreValue;
    private string hiScoreKey = "HighScore";
    
    // Start is called before the first frame update
    void Start()
    {
        scoreValue = 0;
        
        hiScoreValue = PlayerPrefs.GetInt(hiScoreKey, 0); // so high score persists
        string formattedScore = hiScoreValue.ToString("D4");
        hiScoreText.text = $"HiScore: {formattedScore}";
        
        Enemy.OnEnemyDied += EnemyOnOnEnemyDied;
    }
    
    private void OnDestroy()
    {
        Enemy.OnEnemyDied -= EnemyOnOnEnemyDied;
    }
    
    void EnemyOnOnEnemyDied(int points)
    {
        // Debug.Log("player received EnemyDied. got " + points + " points");
        scoreValue += points;
        string formattedScore = scoreValue.ToString("D4"); // so score will be 4 digits w/ leading zeroes
        scoreText.text = $"Score: {formattedScore}";
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreValue > hiScoreValue)
        {
            hiScoreValue = scoreValue;
            PlayerPrefs.SetInt(hiScoreKey, hiScoreValue);
            PlayerPrefs.Save();
            
            string formattedScore = hiScoreValue.ToString("D4");
            hiScoreText.text = $"HiScore: {formattedScore}";
        }
    }
}
