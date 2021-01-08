
using UnityEngine;

/// <summary>
/// State for the state machine that makes up the AI loop
/// </summary>
/// <param name="actions">The actions that the state performs</param>
/// <param name="transitions">The transitions the state supports</param>
/// <param name="sceneGizmoColor">The gizmo color for the scene</param>
[CreateAssetMenu(menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    public Actions[] actions;
    public Transition[] transitions;
    public Color sceneGizmoColor;

    /// <summary>
    /// Performs the actions and transitions for the scene
    /// </summary>
    /// <param name="controller"></param>
    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(StateController controller)
    {
        for(int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    void CheckTransitions(StateController controller)
    {
        for(int i = 0; i < transitions.Length; i++)
        {
            bool decisionSucceeded = transitions[i].decision.Decide(controller);
            if (decisionSucceeded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }
}
