using UnityEngine;

public class OnDifficultySelected : IEvent
{
    Difficulty difficulty = Difficulty.Easy;
    public void Set(params object[] data)
    {
        difficulty = (Difficulty)data[0];
    }

    public void Reset()
    {
        difficulty = Difficulty.Easy;
    }
}
