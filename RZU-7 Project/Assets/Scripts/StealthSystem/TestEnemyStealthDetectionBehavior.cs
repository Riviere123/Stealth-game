using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyStealthDetectionBehavior : MonoBehaviour
{
    public bool spotted;
    public float rotationSpeed;
    public GameObject target;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!spotted)
        {
            transform.Rotate(0, 0, .1f * rotationSpeed);
        }
        if (spotted)
        {
            if (target)
            {
                
                var dir = target.transform.position - transform.position;
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            }           
        }
    }

}
