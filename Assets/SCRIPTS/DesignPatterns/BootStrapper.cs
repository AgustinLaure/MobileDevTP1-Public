using UnityEngine;

public static class BootStrapper
{
    private static ServiceLocator serviceLocator = ServiceLocator.Instance;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        serviceLocator.AddService(new CompositePool());
        serviceLocator.AddService(new EventBus());
    }
}
