using UnityEngine;

[RequireComponent(typeof(StateController))]
public abstract class StateMonoHelper : MonoBehaviour
{
    protected StateController controller;

    private void Awake()
    {
        controller = gameObject.GetComponent<StateController>();
        if (controller == null)
        {
            Debug.LogError("Did not find expected StateController component in " + gameObject.name + ".");
        }
    }
}
