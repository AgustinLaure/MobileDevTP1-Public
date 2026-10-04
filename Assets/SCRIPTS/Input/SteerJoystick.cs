using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

public class SteerJoystick : OnScreenControl, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [InputControl(layout = "Axis")]
    [SerializeField] private string m_ControlPath;

    private RectTransform rectTransform;
    private float currAngle = 0;
    private float maxAngle = 90f;
    private float snapSpeed = 240f;

    private bool isTouching = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ProcessTouch(eventData);
        isTouching = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ProcessTouch(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouching = false;
        SendValueToControl(0.0f);
    }

    private void ProcessTouch(PointerEventData eventData)
    {
        Vector2 pointerPos = eventData.position;
        Vector2 rectTransformCenter = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, rectTransform.position);
        Vector2 direction = pointerPos - rectTransformCenter;

        float angle = Vector2.SignedAngle(Vector2.up, direction);

        currAngle = Mathf.Clamp(angle, -maxAngle, maxAngle);

        rectTransform.localEulerAngles = new Vector3(0f, 0f, currAngle);

        SendValueToControl(-currAngle / maxAngle);
    }

    private void Update()
    {
        if (!isTouching && currAngle != 0f)
        {
            currAngle = Mathf.MoveTowardsAngle(currAngle, 0f, snapSpeed * Time.deltaTime);
            rectTransform.localEulerAngles = new Vector3(0f, 0f, currAngle);
        }
    }
}
