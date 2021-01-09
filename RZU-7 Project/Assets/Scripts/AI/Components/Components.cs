using System;
using System.Reflection;
using UnityEngine;

public abstract class Components : MonoBehaviour
{
    public T Get<T>(string name)
    {
        Debug.Log("STARTING CALL");
        foreach (var value in GetType().GetFields())
        {
            Debug.Log("TYPE " + value);
            Debug.Log("NAME " + value.Name);
            Debug.Log("COMPARE TYPE " + typeof(T).Equals(value.FieldType));
            Debug.Log("COMPARE NAME " + value.Name.Equals(name));
            Debug.Log("FULL COMPARISON " + (typeof(T).Equals(value.FieldType) && value.Name.Equals(name)));
            if (typeof(T).Equals(value.FieldType) && value.Name.Equals(name))
            {
                FieldInfo test = GetType().GetField(name);
                return (T)test.GetValue(this);
            }
        }

        return default;
    }


}
