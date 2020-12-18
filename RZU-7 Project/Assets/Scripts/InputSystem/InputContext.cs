using UnityEngine;

public abstract class InputContext : MonoBehaviour
{
    public abstract void HorizontalButtonPress(float value);

    public abstract void VerticalButtonPress(float value);

    public abstract void Action1ButtonPress();

    public abstract void Action2ButtonPress();

    public abstract void Menu1ButtonPress();

    public abstract void Menu2ButtonPress();
}
