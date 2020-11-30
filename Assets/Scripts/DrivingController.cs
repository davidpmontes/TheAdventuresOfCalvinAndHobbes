using UnityEngine;

public class DrivingController : MonoBehaviour
{
    public static DrivingController Instance { get; private set; }

    private Vector2 leftStickInput;
    private bool accelerateInput;
    private bool brakeInput;

    private UnityEngine.InputSystem.PlayerInput playerInput;

    protected Animator animator;
    protected DIRECTION direction = DIRECTION.DOWN;
    protected MovingObject movingObject;

    private float maxSpeed = 50f;
    private float maxTurnSpeed = 3f;
    private Vector2 velocity;

    public float speed = 0;

    private void Awake()
    {
        Instance = this;
        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        animator = GetComponent<Animator>();
        movingObject = GetComponent<MovingObject>();
    }

    void Update()
    {
        GetInput();
        CalculatePhysics();
    }

    private void FixedUpdate()
    {
        movingObject.MoveSmoothly(velocity);
    }

    private void GetInput()
    {
        leftStickInput = playerInput.actions["Steer"].ReadValue<Vector2>();
        accelerateInput = playerInput.actions["Accelerate"].ReadValue<float>() > 0;
        brakeInput = playerInput.actions["Brake"].ReadValue<float>() > 0;
    }

    private void CalculatePhysics()
    {
        // steering
        transform.rotation *= Quaternion.Euler(0, 0, -leftStickInput.x * Mathf.Abs(velocity.magnitude / maxSpeed) * maxTurnSpeed);

        //tire drift friction
        var tireFrictionAmount = Mathf.Abs(Vector2.Dot(transform.right, velocity));
        velocity += -velocity.normalized * tireFrictionAmount * Mathf.Abs(velocity.magnitude / maxSpeed) * 5 * Time.deltaTime;

        // accelerating / braking
        if (accelerateInput)
        {
            velocity += (Vector2)transform.up * 15 * Time.deltaTime;
            //speed = Mathf.MoveTowards(speed, maxSpeed, 10 * Time.deltaTime);
        }
        else
        {
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, Time.deltaTime);
            //speed = Mathf.MoveTowards(speed, 0, 10 * Time.deltaTime);
        }
    }
}
