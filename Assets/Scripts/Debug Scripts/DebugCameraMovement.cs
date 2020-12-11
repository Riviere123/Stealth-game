using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCameraMovement : MonoBehaviour
{
    //THIS SCRIPT IS A FREE MOVING CAMERA PUT THIS ON THE MAIN CAMERA IN THE SCENE TO USE IT.
    //MOVE WITH WASD CAN CHANGE THE SPEED WITH SPEED VARIABLE.
    //IF YOU WANT THE CAMERA TO FOLLOW THE TARGET INSTEAD SET A TARGET VARIABLE;


    public float speed = 10;
    public GameObject target;
    public KeyCode up;
    public KeyCode right;
    public KeyCode down;
    public KeyCode left;
    public KeyCode zoomIn;
    public KeyCode zoomOut;
    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        if (!target)
        {
            if (Input.GetKey(up))
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            }
            if (Input.GetKey(right))
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
            if (Input.GetKey(down))
            {
                transform.Translate(Vector3.down * Time.deltaTime * speed);
            }
            if (Input.GetKey(left))
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }
        if (target)
        {
            transform.position = target.transform.position + new Vector3(0, 0, -10);
        }
        if (Input.GetKeyDown(zoomIn))
        {
            cam.orthographicSize++;
        }
        if (Input.GetKeyDown(zoomOut))
        {
            cam.orthographicSize--;
        }
    }
    
}
