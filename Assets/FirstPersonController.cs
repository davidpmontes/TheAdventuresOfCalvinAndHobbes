using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public static FirstPersonController Instance { get; private set; }

    private Vector2 leftStickInput;
    private Vector2 rightStickInput;

    private UnityEngine.InputSystem.PlayerInput playerInput;

    protected Animator animator;
    protected DIRECTION direction = DIRECTION.DOWN;
    protected MovingObject movingObject;

    private Vector2 velocity;

    public readonly float maxForwardSpeed = 20f;
    public readonly float maxReverseSpeed = 10f;
    public readonly float maxStrafeSpeed = 10f;
    public readonly float maxTurnSpeed = 100f;

    private GameObject cameraRotate;
    private GameObject calvinRotate;

    private string UP = "up";
    private string DOWN = "down";
    private string LEFT = "left";
    private string RIGHT = "right";
    private string facingDirection;

    private void Awake()
    {
        Instance = this;
        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        animator = GetComponent<Animator>();
        movingObject = GetComponent<MovingObject>();
        calvinRotate = Utils.FindChildByNameRecursively(transform, "CalvinRotate");
        cameraRotate = Utils.FindChildByNameRecursively(transform, "CameraRotate");
        facingDirection = UP;
    }

    void Update()
    {
        GetInput();
        Move();
        Look();
    }

    private void FixedUpdate()
    {
        movingObject.MoveSmoothly(velocity);
    }

    private void GetInput()
    {
        leftStickInput = playerInput.actions["FPSMove"].ReadValue<Vector2>();
        rightStickInput = playerInput.actions["FPSLook"].ReadValue<Vector2>();
    }

    private void Move()
    {
        var maxSpeed = leftStickInput.y < 0 ? maxReverseSpeed : maxForwardSpeed;

        velocity = calvinRotate.transform.up * leftStickInput.y * maxSpeed +
                   calvinRotate.transform.right * leftStickInput.x * maxStrafeSpeed;
    }

    private void Look()
    {
        calvinRotate.transform.rotation *= Quaternion.Euler(0, 0, -rightStickInput.x * maxTurnSpeed * Time.deltaTime);
        cameraRotate.transform.rotation *= Quaternion.Euler(0, 0, -rightStickInput.x * maxTurnSpeed * Time.deltaTime);
    }

    public Vector2 GetVelocity() => velocity;

    public Vector2 GetLeftInput() => leftStickInput;

    public string GetFacingDirection()
    {

        // more left/right
        if (Mathf.Abs(leftStickInput.x) > Mathf.Abs(leftStickInput.y))
        {
            if (leftStickInput.x > 0) facingDirection = RIGHT;
            else facingDirection = LEFT;
        }
        else if (Mathf.Abs(leftStickInput.x) < Mathf.Abs(leftStickInput.y))
        {
            if (leftStickInput.y > 0) facingDirection = UP;
            else facingDirection = DOWN;
        }

        return facingDirection;
    }
}
