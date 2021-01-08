using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralHelper : StateMonoHelper
{
    [Header("General")]
    public Rigidbody2D rigidBody;
    public Collider2D collider2d;
    public AIStats stats;

    private void Start()
    {
        if (rigidBody == null)
        {
            rigidBody = gameObject.GetComponent<Rigidbody2D>();
        }

        if (collider2d == null)
        {
            collider2d = gameObject.GetComponent<Collider2D>();
        }

        if (stats == null)
        {
            stats = gameObject.GetComponent<AIStats>();
        }
    }
}
