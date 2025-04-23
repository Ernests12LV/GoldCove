using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement speed variables
    public float moveSpeed = 5f;
    public float sprintSpeed = 7f;
    public float jumpForce = 5f;

    // Mouse sensitivity
    public float mouseSensitivityX = 2f;
    public float mouseSensitivityY = 2f;

    // Rotation limits
    public float minLookAngle = -60f;
    public float maxLookAngle = 60f;

    private float rotationX = 0f;
    private Rigidbody rb;
    public Camera playerCamera;
    //private GameObject playerCamera;

    public bool onLand = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {   
        if(onLand){
        // Handle mouse look (horizontal and vertical)
        HandleMouseLook();

        // Handle player movement (forward/backward/strafe)
        HandleMovement();
        }
        
    }

    public void EnableMovement(){
        onLand = true;
        playerCamera.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DisableMovement(){
        onLand = false;
        playerCamera.gameObject.SetActive(false);
    }

    void HandleMouseLook()
    {
        // Get the mouse input for X and Y axis
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY;

        // Rotate the camera horizontally
        transform.Rotate(0, mouseX, 0);

        // Rotate the player vertically, clamping the rotation to prevent flipping
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minLookAngle, maxLookAngle);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }

    void HandleMovement()
    {
        // Get input for movement (WASD or arrow keys)
        float moveDirectionY = Input.GetAxis("Vertical");
        float moveDirectionX = Input.GetAxis("Horizontal");

        // Get the movement vector (relative to player rotation)
        Vector3 move = transform.right * moveDirectionX + transform.forward * moveDirectionY;

        // Check if sprinting
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        // Apply movement using Rigidbody
        Vector3 velocity = move * currentSpeed;
        velocity.y = rb.linearVelocity.y; // Preserve the current Y velocity (important for jump/gravity)
        rb.linearVelocity = velocity;
    }

    void FixedUpdate()
    {
        // Apply jump force (this is done in FixedUpdate for physics calculations)
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        // Check if the player is grounded (use a simple raycast to detect)
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
