using UnityEngine;

public abstract class StateMonoHelper : MonoBehaviour
{
    protected string Name;

    public string GetName()
    {
        return Name;
    }

    public void SetName(string name)
    {
        Name = name;
    }
}
