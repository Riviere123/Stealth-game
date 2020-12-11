using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMonitor : MonoBehaviour
{
    public Constants.InputContexts currentContextEnum;
    public InputContext currentContextClass;
    public MenuContext menuContext = new MenuContext();
    public GameplayContext gameplayContext = new GameplayContext();

    void Start()
    {
        SetInputContext(Constants.InputContexts.menu);
    }

    void Update()
    {
        float horizInput = Input.GetAxis(Constants.horizontal);
        if (!Mathf.Approximately(Mathf.Abs(horizInput), 0f))
        {
            currentContextClass.HorizontalButtonPress(horizInput);
        }

        float verticalInput = Input.GetAxis(Constants.vertical);
        if (!Mathf.Approximately(Mathf.Abs(verticalInput), 0f))
        {
            currentContextClass.VerticalButtonPress(verticalInput);
        }

        if (Input.GetButtonDown(Constants.action1))
        {
            currentContextClass.Action1ButtonPress();
        }

        if (Input.GetButtonDown(Constants.action2))
        {
            currentContextClass.Action2ButtonPress();
        }

        if (Input.GetButtonDown(Constants.menu1))
        {
            currentContextClass.Menu1ButtonPress();
        }

        if (Input.GetButtonDown(Constants.menu2))
        {
            currentContextClass.Menu2ButtonPress();
        }
    }

    public void SetInputContext(Constants.InputContexts context)
    {
        switch(context)
        {
            case Constants.InputContexts.menu:
                Debug.Log("Selected \"" + Constants.menu + "\" context");
                SetMenuContext();
                break;

            case Constants.InputContexts.gameplay:
                Debug.Log("Selected \"" + Constants.gameplay + "\" context");
                SetGameplayContext();
                break;

            default:
                Debug.Log("Defaulted to \"" + Constants.gameplay + "\" context");
                SetGameplayContext();
                break;
        }
    }

    public void SetMenuContext()
    {
        currentContextClass = menuContext;
    }

    public void SetGameplayContext()
    {
        currentContextClass = gameplayContext;
    }
}
