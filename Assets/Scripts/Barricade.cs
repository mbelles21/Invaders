using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public float health = 10f;
    public float scaleFactor = 0.1f;
    public ParticleSystem particles;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        health--;
        
        // Debug.Log("barricade hit " + health);
        Destroy(collision.gameObject);
        
        particles.Play();
        GetComponent<AudioSource>().Play();

        Vector3 currentScale = transform.localScale;
        currentScale.y *= 1.0f - scaleFactor;
        transform.localScale = currentScale;
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
