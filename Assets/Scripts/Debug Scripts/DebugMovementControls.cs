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
        Vector3 worldPosition = Input.mousePosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var angle = Mathf.Atan2(worldPosition.x, worldPosition.y) * Mathf.Rad2Deg;
        xval = Input.GetAxis("Horizontal");
        yval = Input.GetAxis("Vertical");
        rb2d.SetRotation(angle);

    }

    private void FixedUpdate()
    {
        rb2d.AddForce(new Vector2(xval, yval) * speed);
    }
}
