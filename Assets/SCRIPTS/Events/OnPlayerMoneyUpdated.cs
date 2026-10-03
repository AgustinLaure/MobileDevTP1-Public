using UnityEngine;

public class OnPlayerMoneyUpdated : IEvent
{
    public Player player = null;
    public int money = 0;

    public void Set(params object[] data)
    {
        player = data[0] as Player;
        money = (int)data[1];
    }

    public void Reset()
    {
        player = null;
        money = 0;
    }
}
