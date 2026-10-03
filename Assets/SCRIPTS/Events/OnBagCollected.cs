using UnityEngine;

public class OnBagCollected : IEvent
{
    public Player player = null;
    public int bagsAmount = 0;

    public void Set(params object[] data)
    {
        player = data[0] as Player;
        bagsAmount = (int)data[1];
    }

    public void Reset()
    {
        player = null;
        bagsAmount = 0;
    }
}
