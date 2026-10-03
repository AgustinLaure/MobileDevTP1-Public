using UnityEngine;

public class OnPlayerWaitingTextShouldUpdate : IEvent
{
    public Player player = null;
    public bool state = false;

    public void Set(params object[] data)
    {
        player = data[0] as Player;
        state = (bool)data[1];
    }

    public void Reset()
    {
        player = null;
        state = false;
    }
}
