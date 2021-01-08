using UnityEngine;

[System.Serializable]
public class MonoHelperSet
{
    [SerializeField]
    private string Name;
    [SerializeField]
    private StateMonoHelper helper;

    public string GetName()
    {
        return Name;
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public StateMonoHelper GetHelper()
    {
        return helper;
    }

    public void SetHelper(StateMonoHelper helper)
    {
        this.helper = helper;
    }
}
