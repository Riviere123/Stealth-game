using UnityEngine;

public class TestInputMonitor : MonoBehaviour
{
    [SerializeField]
    InputConstants.InputContexts currentContextEnum;
    [SerializeField]
    InputContext currentContextClass;
    [SerializeField]
    MenuContext menuContext;
    [SerializeField]
    TestGameplayContext gameplayContext;

    void Update()
    {
        currentContextClass.HorizontalButtonPress(Input.GetAxis(InputConstants.horizontal));
        currentContextClass.VerticalButtonPress(Input.GetAxis(InputConstants.vertical));

        if (Input.GetButtonDown(InputConstants.action1))
        {
            currentContextClass.Action1ButtonPress();
        }

        if (Input.GetButtonDown(InputConstants.action2))
        {
            currentContextClass.Action2ButtonPress();
        }

        if (Input.GetButtonDown(InputConstants.menu1))
        {
            currentContextClass.Menu1ButtonPress();
        }

        if (Input.GetButtonDown(InputConstants.menu2))
        {
            currentContextClass.Menu2ButtonPress();
        }
    }

    public void SetInputContext(InputConstants.InputContexts context)
    {
        switch (context)
        {
            case InputConstants.InputContexts.menu:
                //Debug.Log("Selected \"" + InputConstants.menu + "\" context");
                SetMenuContext();
                break;

            case InputConstants.InputContexts.gameplay:
                //Debug.Log("Selected \"" + InputConstants.gameplay + "\" context");
                SetGameplayContext();
                break;

            default:
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
