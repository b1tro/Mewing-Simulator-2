using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mewing : MonoBehaviour
{
    [Range(0, 1000)]
    public float speed;
    
    private void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        
        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        rb.MovePosition(Vector3.Normalize(diference) * speed);
    }
}
