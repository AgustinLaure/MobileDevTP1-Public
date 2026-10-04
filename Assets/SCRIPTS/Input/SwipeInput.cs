using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

public class SwipeInput : OnScreenControl, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [InputControl(layout = "Vector2")]
    [SerializeField] private string m_ControlPath;

    private Vector2 pointerDownPosition = Vector2.zero;
    private Vector2 pointerUpPosition = Vector2.zero;


    private void Awake()
    {

    }

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerUpPosition = eventData.position;

        ProcessTouch(eventData);
    }

    private void ProcessTouch(PointerEventData eventData)
    {
        Vector2 swipeDirection = Vector3.Normalize(pointerUpPosition - pointerDownPosition);

        float verticalDot = Vector3.Dot(swipeDirection, Vector3.up);
        float horizontalDot = Vector3.Dot(swipeDirection, Vector3.right);

        Vector2 result = Vector2.zero;

        if (verticalDot >= 0.75f)
        {
            result.y = 1f;
        }
        else if (verticalDot <= -0.75f)
        {
            result.y = -1f;
        }
        else if (horizontalDot >= 0.75f)
        {
            result.x = 1f;
        }
        else if (horizontalDot <= -0.75f)
        {
            result.x = -1f;
        }

        SendValueToControl(result);
    }

    private void Update()
    {

    }
}

