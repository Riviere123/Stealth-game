using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponents : Components
{
    [Header("General")]
    public Rigidbody2D rigidBody;
    public Collider2D collider2d;
    public AIStats stats;
}
