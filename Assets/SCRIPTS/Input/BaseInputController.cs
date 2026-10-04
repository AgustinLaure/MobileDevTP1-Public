using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseInputController
{
    protected InputClass inputClass;

    public BaseInputController(string mask)
    {
        inputClass = new InputClass();

        inputClass.bindingMask = InputBinding.MaskByGroup(mask);

        inputClass.Player.Enable();
    }

    public abstract float GetSteering();

    public abstract bool GetUpPressed();
    public abstract bool GetDownPressed();
    public abstract bool GetLeftPressed();
    public abstract bool GetRightPressed();
}
