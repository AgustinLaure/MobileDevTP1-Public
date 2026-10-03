using UnityEngine;

public class TruckHUD : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    public void SetCanvasCamera(Camera camera)
    {
        canvas.worldCamera = camera;
    }
}
