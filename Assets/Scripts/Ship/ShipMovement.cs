using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float rotationSpeedTransitionTime = 0.5f;

    public int currentSpeedIndex = 0;
    public float[] speeds = { 1f, 5f, 10f };
    public float backwardSpeed = 1f;
    public float acceleration = 2.5f;
    public float deceleration = 2.5f;
    public float currentSpeed = 0f;
    public float targetSpeed = 0f;
    private float currentRotationSpeed = 0f;

    public bool isMovementEnabled = false;
    public bool isMovingForward = false;
    public bool isMovingBackward = false;
    public bool isStopping = false;

    private void Update()
    {
        if (!isMovementEnabled) return;

        HandleInput();
        UpdateSpeed();
        UpdateRotation();
        ApplyMovement();
    }

    private void HandleInput()
    {
        float rotationInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isMovingBackward)
            {
                isMovingBackward = false;
                isStopping = true;
            }
            else if (!isMovingForward && !isMovingBackward)
            {
                isMovingForward = true;
                currentSpeedIndex = 0;
            }
            else if (isMovingForward && currentSpeedIndex < speeds.Length - 1)
            {
                currentSpeedIndex++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (isMovingForward)
            {
                if (currentSpeedIndex > 0)
                {
                    currentSpeedIndex--;
                    targetSpeed = speeds[currentSpeedIndex];
                    isStopping = true;
                }
                else
                {
                    isMovingForward = false;
                    isStopping = true;
                }
            }
            else if (!isMovingForward && !isMovingBackward)
            {
                isMovingBackward = true;
            }
        }

        // Apply rotation input
        transform.Rotate(Vector3.up, rotationInput * currentRotationSpeed * Time.deltaTime);
    }

    private void UpdateSpeed()
    {
        if (isStopping)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, deceleration * Time.deltaTime);
            if (Mathf.Approximately(currentSpeed, targetSpeed))
            {
                isStopping = false;
                currentSpeed = isMovingForward ? speeds[currentSpeedIndex] : -backwardSpeed;
            }
        }
        else
        {
            if (isMovingForward)
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, speeds[currentSpeedIndex], acceleration * Time.deltaTime);
            }
            else if (isMovingBackward)
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, -backwardSpeed, acceleration * Time.deltaTime);
            }
            else
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
            }
        }
    }

    private void UpdateRotation()
    {
        if (isMovingForward || isMovingBackward)
        {
            float targetRotationSpeed = rotationSpeed * Mathf.Sign(currentSpeed) * (1f / Mathf.Max(Mathf.Abs(currentSpeed), 0.001f));
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, targetRotationSpeed, rotationSpeedTransitionTime * Time.deltaTime);
        }
        else
        {
            currentRotationSpeed = 0f;
        }
    }

    private void ApplyMovement()
    {
        Vector3 movement = transform.forward * currentSpeed * Time.deltaTime;
        transform.position += movement;
    }

    public void EnableMovement()
    {
        isMovementEnabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DisableMovement()
    {
        isMovementEnabled = false;
        currentSpeed = 0f;
        isMovingForward = false;
        isMovingBackward = false;
        isStopping = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
