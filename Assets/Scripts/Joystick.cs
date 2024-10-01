// using UnityEngine;
// using UnityEngine.EventSystems;

// public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
// {
//     private RectTransform joystickBackground;  // The background (outer circle)
//     private RectTransform joystickHandle;      // The handle (inner circle)
//     private Vector2 inputVector;               // Stores the input direction (X and Y)

//     private float joystickRadius;              // Radius of the joystick background

//     private void Start()
//     {
//         // Get the RectTransform of the joystick background and handle
//         joystickBackground = GetComponent<RectTransform>();
//         joystickHandle = transform.GetChild(0).GetComponent<RectTransform>();

//         // Calculate the radius of the joystick background (half the width/height, assuming it's a circle)
//         joystickRadius = joystickBackground.sizeDelta.x / 2;
//     }

//     // Called when the user drags the joystick handle
//     public void OnDrag(PointerEventData eventData)
//     {
//         Vector2 position;
//         // Converts the screen point to local point relative to the joystick background
//         RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out position);

//         // Normalize the input so it's between -1 and 1, and limit it to the joystick's radius
//         inputVector = position / joystickRadius;
//         inputVector = (inputVector.magnitude > 1) ? inputVector.normalized : inputVector;  // Limit to circle's edge

//         // Move the joystick handle within the allowed range (clamped to the radius)
//         joystickHandle.anchoredPosition = inputVector * joystickRadius;
//     }

//     // Called when the joystick is first touched
//     public void OnPointerDown(PointerEventData eventData)
//     {
//         OnDrag(eventData);  // Trigger drag behavior immediately when the user touches the joystick
//     }

//     // Called when the user lifts their finger off the joystick
//     public void OnPointerUp(PointerEventData eventData)
//     {
//         ResetJoystick();  // Call the reset function
//     }

//     // Resets the joystick handle to the center and stops any input
//     private void ResetJoystick()
//     {
//         Debug.Log("wer");
//         inputVector = Vector2.zero;  // Reset the input vector to (0, 0)
//         joystickHandle.anchoredPosition = Vector2.zero;  // Return the joystick handle to the center of the background
//     }

//     // Returns the horizontal input (X-axis movement)
//     public float Horizontal()
//     {
//         return inputVector.x;
//     }

//     // Returns the vertical input (Y-axis movement)
//     public float Vertical()
//     {
//         return inputVector.y;
//     }
// }
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform joystickBackground;  // The background (outer circle)
    private RectTransform joystickHandle;      // The handle (inner circle)
    private Vector2 inputVector;               // Stores the input direction (X and Y)

    private float joystickRadius;              // Radius of the joystick background
    private Vector2 initialHandlePosition;     // Store the initial position of the joystick handle

    private void Start()
    {
        // Get the RectTransform of the joystick background and handle
        joystickBackground = GetComponent<RectTransform>();
        joystickHandle = transform.GetChild(0).GetComponent<RectTransform>();

        // Calculate the radius of the joystick background (half the width/height, assuming it's a circle)
        joystickRadius = joystickBackground.sizeDelta.x / 2;

        // Store the initial position of the joystick handle
        initialHandlePosition = joystickHandle.anchoredPosition;
    }

    // Called when the user drags the joystick handle
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        // Converts the screen point to local point relative to the joystick background
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out position);

        // Calculate the relative position from the initial handle position
        Vector2 relativePosition = position - initialHandlePosition;

        // Normalize the input so it's between -1 and 1, and limit it to the joystick's radius
        inputVector = relativePosition / joystickRadius;
        inputVector = (inputVector.magnitude > 1) ? inputVector.normalized : inputVector;  // Limit to circle's edge

        // Move the joystick handle relative to the initial position within the allowed range (clamped to the radius)
        joystickHandle.anchoredPosition = initialHandlePosition + (inputVector * joystickRadius);
    }

    // Called when the joystick is first touched
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);  // Trigger drag behavior immediately when the user touches the joystick
    }

    // Called when the user lifts their finger off the joystick
    public void OnPointerUp(PointerEventData eventData)
    {
        ResetJoystick();  // Call the reset function
    }

    // Resets the joystick handle to the initial position
    private void ResetJoystick()
    {
        inputVector = Vector2.zero;  // Reset the input vector to (0, 0)
        joystickHandle.anchoredPosition = initialHandlePosition;  // Reset to initial handle position
    }

    // Returns the horizontal input (X-axis movement)
    public float Horizontal()
    {
        return inputVector.x;
    }

    // Returns the vertical input (Y-axis movement)
    public float Vertical()
    {
        return inputVector.y;
    }
}
