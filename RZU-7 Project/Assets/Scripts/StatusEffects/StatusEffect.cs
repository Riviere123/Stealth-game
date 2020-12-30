using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public string statusName;
    public abstract void Trigger();
}
