using UnityEngine;

public class DroneController : MonoBehaviour
{
    public Joystick movementJoystick;  // For forward, backward, left, and right movement
    public Joystick verticalJoystick;  // For up and down movement
    public float movementSpeed = 5f;
    public float verticalSpeed = 3f;

    void Update()
    {
        // Get input from the movement joystick (X: left/right, Y: forward/backward)
        float moveHorizontal = movementJoystick.Horizontal();
        float moveVertical = movementJoystick.Vertical();

        // Get input from the vertical joystick (Y axis for up/down movement)
        float moveUp = verticalJoystick.Vertical();

        // Calculate movement vectors
        Vector3 horizontalMovement = new Vector3(moveHorizontal, 0f, moveVertical) * movementSpeed * Time.deltaTime;
        Vector3 verticalMovement = new Vector3(0f, moveUp, 0f) * verticalSpeed * Time.deltaTime;

        // Apply movement to the drone
        transform.Translate(horizontalMovement + verticalMovement);
    }
}
