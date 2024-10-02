using UnityEngine;

public class DroneController : MonoBehaviour
{
    public Joystick movementJoystick;  // Horizontal joystick for global X and Z movement
    public Joystick verticalJoystick;  // Vertical joystick for altitude (up/down) and rotation along Z-axis (roll)
    public float movementSpeed = 5f;   // Speed for global X and Z movement
    public float verticalSpeed = 3f;   // Speed for altitude movement
    public float rotationSpeed = 100f; // Speed for rotating the drone (roll along Z-axis)
    public float dragFactor = 0.95f;   // Drag for reducing speed when no input is given
    public float gravityScale = 9.81f; // Simulated gravity

    private Rigidbody rb;              // Rigidbody reference for applying forces

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;  // Turn off Unity's default gravity to use custom gravity logic
    }

    void Update()
    {
        // Handle horizontal joystick movement (global X and Z movement)
        float verticalInput = movementJoystick.Vertical();   // Vertical input should move along the global Z-axis
        float horizontalInput = movementJoystick.Horizontal();  // Horizontal input should move along the global X-axis

        // Move along the global Z-axis based on vertical input (up/down on the joystick moves forward/backward)
        Vector3 globalZMovement = Vector3.forward * verticalInput * movementSpeed;  // Moves along Z-axis
        // Move along the global X-axis based on horizontal input (left/right on the joystick moves left/right)
        Vector3 globalXMovement = Vector3.right * horizontalInput * movementSpeed;  // Moves along X-axis

        // Apply forces to move the drone along the global X and Z axes
        rb.AddForce(globalXMovement + globalZMovement);

        // Handle vertical joystick (altitude control and rotation around Z-axis)
        float altitude = verticalJoystick.Vertical();  // Altitude control
        float rollInput = verticalJoystick.Horizontal();  // Roll rotation control (along Z-axis)

        // Apply altitude movement
        Vector3 altitudeMovement = Vector3.up * altitude * verticalSpeed;
        rb.AddForce(altitudeMovement);

        // Simulate gravity when the joystick is neutral (not moving up or down)
        if (Mathf.Abs(altitude) < 0.1f)
        {
            rb.AddForce(Vector3.down * gravityScale, ForceMode.Acceleration);
        }

        // Apply roll rotation (rotate the drone left or right along Z-axis based on vertical joystick input)
        RotateDrone(rollInput);
    }

    // Rotate the drone based on roll input from the vertical joystick (rotation along Z-axis)
    private void RotateDrone(float rollInput)
    {
        if (Mathf.Abs(rollInput) > 0.1f)  // Apply rotation only if the input is significant
        {
            float rollRotation = rollInput * rotationSpeed * Time.deltaTime;

            // Apply rotation around the Z-axis (roll)
            Quaternion deltaRotation = Quaternion.Euler(0f, 0f, rollRotation);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }
}
