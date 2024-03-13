using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public delegate void PlayerDied();
    public static event PlayerDied OnPlayerDied;
    
    public GameObject bullet;
    public Transform shootingOffset;
    public ParticleSystem shotParticles;
    public float speed = 5f;
    public float maxX = 10f;
    public float minX = -10f;

    private void Start()
    {
        Enemy.OnEndReached += EnemyOnOnEndReached;
    }

    private void OnDestroy()
    {
        Enemy.OnEndReached -= EnemyOnOnEndReached;
    }

    void EnemyOnOnEndReached()
    {
        OnPlayerDied.Invoke();
        GetComponent<Animator>().SetTrigger("Dies");
    }

    // Update is called once per frame
    void Update()
    {
      float direction = Input.GetAxis("Horizontal");
      Vector3 movement = new Vector3(direction, 0f, 0f);
      transform.Translate(movement * speed * Time.deltaTime);
      
      Vector3 currentPos = transform.position;
      currentPos.x = Mathf.Clamp(currentPos.x, minX, maxX);
      transform.position = currentPos;
      
      
      if (Input.GetKeyDown(KeyCode.Space))
      {
        GetComponent<Animator>().SetTrigger("Shoot Trigger");
        
        shotParticles.Play();
        
        GameObject shot = Instantiate(bullet, shootingOffset.position, Quaternion.identity);
        Debug.Log("Bang!");

        Destroy(shot, 3f);

      }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        OnPlayerDied.Invoke();
        GetComponent<Animator>().SetTrigger("Dies");
    }

    void Explode()
    {
        Destroy(gameObject);
    }
}
