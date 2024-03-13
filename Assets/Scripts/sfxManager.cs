using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Player.OnPlayerDied += PlayerOnOnPlayerDied;
        Enemy.OnEnemyDied += EnemyOnOnEnemyDied;
    }

    private void OnDestroy()
    {
        Player.OnPlayerDied -= PlayerOnOnPlayerDied;
        Enemy.OnEnemyDied -= EnemyOnOnEnemyDied;
    }

    void PlayerOnOnPlayerDied()
    {
        GetComponent<AudioSource>().Play();
    }

    void EnemyOnOnEnemyDied(int x)
    {
        GetComponent<AudioSource>().Play();
    }
}
