using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCabinetInteraction : Interaction
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInteract(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("The player has opened the cabinet");
        }
        else
        {
            Debug.Log("The player has closed the cabinet");
        }
        
    }
}
