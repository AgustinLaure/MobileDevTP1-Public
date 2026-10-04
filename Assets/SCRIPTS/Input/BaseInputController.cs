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

    public abstract Vector2 GetAxis();
}
