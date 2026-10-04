using UnityEngine;

public class InputSystemController : BaseInputController
{
    public InputSystemController(string mask) : base(mask)
    {

    }

    public override float GetSteering()
    {
        return inputClass.Player.Steering.ReadValue<float>();
    }

    public override Vector2 GetAxis()
    {
        return inputClass.Player.Axis.ReadValue<Vector2>();
    }
}
