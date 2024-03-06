using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootingOffset;
    public float speed = 5f;
    public float maxX = 10f;
    public float minX = -10f;

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
        // GetComponent<Animator>().SetTrigger("Shoot Trigger");
        
        GameObject shot = Instantiate(bullet, shootingOffset.position, Quaternion.identity);
        Debug.Log("Bang!");

        Destroy(shot, 3f);

      }
    }
}
