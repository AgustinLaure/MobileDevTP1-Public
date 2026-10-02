using UnityEngine;

public static class UIUtils
{
    public static void SetCanvasState(CanvasGroup cg, bool isActive)
    {
        cg.alpha = isActive ? 1f : 0f;
        cg.blocksRaycasts = isActive;
        cg.interactable = isActive;
    }
}
