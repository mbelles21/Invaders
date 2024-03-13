using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDied(int points); // so it can talk to other scripts
    public static event EnemyDied OnEnemyDied; // ditto
    
    public delegate void EndReached();
    public static event EndReached OnEndReached;

    public GameObject bullet;
    public float shootingOffset = 0.75f;
    public float shotInterval = 5f;

    public float killZone = -3.25f;
    
    // Start is called before the first frame update
    private void Start()
    {
        String currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Game")
            InvokeRepeating("Shoot", 0f, shotInterval);
    }


    private void Update()
    {
        float posY = transform.position.y;
        if (posY <= killZone)
        {
            // Debug.Log("game over");
            OnEndReached.Invoke();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            return;
        }
        
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
    }

    void Explode()
    {
        Destroy(gameObject);
    }

    void Shoot()
    {
        int rand = Random.Range(1, 101);
        if (rand <= 25)
        {
            Vector3 shotPos = new Vector3(transform.position.x, transform.position.y - shootingOffset, transform.position.z);
            GameObject shot = Instantiate(bullet, shotPos, Quaternion.identity);
            Destroy(shot, 3f);
        }
    }
}
