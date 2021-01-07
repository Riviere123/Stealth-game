using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/Stats")]
public class AIStats : ScriptableObject
{
    [Range(1,100)]
    public float walkSpeed;
    [Range(1, 100)]
    public float runSpeed;
}
