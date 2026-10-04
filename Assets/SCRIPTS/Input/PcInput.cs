using UnityEngine;

public class PcInput : BaseInputController
{
    public PcInput(string mask) : base(mask)
    {

    }

    public override float GetSteering()
    {
        return inputClass.Player.Steering.ReadValue<float>();
    }

    public override bool GetUpPressed()
    {
        return inputClass.Player.MoveUp.IsPressed();
    }

    public override bool GetDownPressed()
    {
        return inputClass.Player.MoveDown.IsPressed();
    }

    public override bool GetLeftPressed()
    {
        return inputClass.Player.MoveLeft.IsPressed();
    }

    public override bool GetRightPressed()
    {
        return inputClass.Player.MoveRight.IsPressed();
    }
}
