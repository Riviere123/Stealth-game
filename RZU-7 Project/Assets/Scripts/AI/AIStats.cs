using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/Stats")]
public class AIStats : ScriptableObject
{
    [Range(.01f,10)]
    public float walkSpeed;
    [Range(.01f, 10)]
    public float runSpeed;
}
