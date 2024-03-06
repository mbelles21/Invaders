using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDied(int points); // so it can talk to other scripts
    public static event EnemyDied OnEnemyDied; // ditto

    public GameObject bullet;
    public float shootingOffset = 0.75f;
    public float shotInterval = 5f;
    
    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("Shoot", 0f, shotInterval);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
      Debug.Log("Ouch!");
      Destroy(collision.gameObject);

      if (gameObject.tag == "type1")
      {
          OnEnemyDied.Invoke(10); // tells the other scripts
      }
      else if (gameObject.tag == "type2")
      {
          OnEnemyDied.Invoke(20);
      }
      else if (gameObject.tag == "type3")
      {
          OnEnemyDied.Invoke(30);
      }
      else if (gameObject.tag == "type4")
      {
          OnEnemyDied.Invoke(100);
      }
      
      GetComponent<Animator>().SetTrigger("Dies");
      //
    }

    void Explode()
    {
        Destroy(gameObject);
    }

    void Shoot()
    {
        Vector3 shotPos = new Vector3(transform.position.x, transform.position.y - shootingOffset, transform.position.z - 1f);
        GameObject shot = Instantiate(bullet, shotPos, Quaternion.identity);
        Destroy(shot, 3f);
    }
}
