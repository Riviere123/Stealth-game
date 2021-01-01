using UnityEngine;

/// <summary>
/// Monitors for user input and executes the corresponding logic of the current
/// input context.
/// </summary>
/// <param name="currentContextEnum">The enum corresponding to the current input context</param>
/// <param name="currentContextClass">The current input class used to interpret user input</param>
/// <param name="menuContext">The input context to interpret user input as menu navigation</param>
/// <param name="gameplayContext">The input context to interpret user input as gameplay inputs</param>
public class InputMonitor : MonoBehaviour
{
    [SerializeField]
    InputConstants.InputContexts currentContextEnum;
    [SerializeField]
    InputContext currentContextClass;
    [SerializeField]
    MenuContext menuContext;
    [SerializeField]
     GameplayContext gameplayContext;

    // Check for all user inputs
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

    /// <summary>
    /// Sets the current input context for the game
    /// </summary>
    /// <param name="context">The context to set the input system to</param>
    public void SetInputContext(InputConstants.InputContexts context)
    {
        switch(context)
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

    /// <summary>
    /// Sets the context to "Menu"
    /// </summary>
    public void SetMenuContext()
    {
        currentContextClass = menuContext;
    }

    /// <summary>
    /// Sets the context to "Gameplay"
    /// </summary>
    public void SetGameplayContext()
    {
        currentContextClass = gameplayContext;
    }
}
