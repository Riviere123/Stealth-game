using System.Reflection;
using UnityEngine;

public abstract class References : MonoBehaviour
{
    public T Get<T>(string name)
    {
        foreach (var value in GetType().GetFields())
        {
            if (typeof(T).Equals(value.FieldType) && value.Name.Equals(name))
            {
                FieldInfo test = GetType().GetField(name);
                return (T)test.GetValue(this);
            }
        }

        return default;
    }


}
