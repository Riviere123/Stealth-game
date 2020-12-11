using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMovementControls : MonoBehaviour
{
    public float speed;
    float xval;
    float yval;
    Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        xval = Input.GetAxis("Horizontal");
        yval = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        rb2d.AddForce(new Vector2(xval, yval) * speed);
    }
}
