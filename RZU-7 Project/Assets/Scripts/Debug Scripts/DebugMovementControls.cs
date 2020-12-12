using UnityEngine;

public class DebugMovementControls : MonoBehaviour
{
    public bool rotateWithMouse;
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
        if (rotateWithMouse)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var dir = worldPosition - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        xval = Input.GetAxis("Horizontal");
        yval = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        rb2d.AddForce(new Vector2(xval, yval) * speed);
    }
    
}
