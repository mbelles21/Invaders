using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public float moveSpeed = 2f;
    private int direction = 1;
    
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(direction * moveSpeed * Time.deltaTime, 0f, 0f);
        transform.Translate(movement);

        float xPos = transform.position.x;
        if (xPos >= 12 || xPos <= -12)
        {
            direction *= -1;
        }
    }
}
