using UnityEngine;

public enum DIRECTION
{
    LEFT,
    RIGHT,
    DOWN,
    UP
}

public abstract class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private Vector2 leftStickInput;
    private bool runInput;
    private bool aimLockInput;

    private UnityEngine.InputSystem.PlayerInput playerInput;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    protected DIRECTION direction = DIRECTION.DOWN;
    protected MovingObject movingObject;


    [SerializeField] private MovingObjectConfig config = default;
    private float speed;

    private void Awake()
    {
        Instance = this;
        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        movingObject = GetComponent<MovingObject>();
        movingObject.AssignConfiguration(config);
    }

    void Update()
    {
        GetInput();
        FlipX();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        speed = runInput ? config.runningSpeed : config.walkingSpeed;
        movingObject.Move(leftStickInput, speed);
    }

    private void GetInput()
    {
        leftStickInput = playerInput.actions["Move"].ReadValue<Vector2>();
        runInput = playerInput.actions["Run"].ReadValue<float>() > 0;
        aimLockInput = playerInput.actions["AimLock"].ReadValue<float>() > 0;
    }

    public virtual void OnAttack()
    {
        animator.SetTrigger("attack");
    }

    private void FlipX()
    {
        if (aimLockInput) return;

        if (direction == DIRECTION.LEFT || direction == DIRECTION.RIGHT)
        {
            if (Mathf.Abs(leftStickInput.x) != 0.0f)
            {
                spriteRenderer.flipX = leftStickInput.x < 0;
            }
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void UpdateAnimator()
    {
        animator.SetBool("isIdle", leftStickInput.magnitude == 0);

        if (leftStickInput.magnitude > 0)
        {
            animator.SetBool("canRun", runInput);

            if (!aimLockInput)
            {
                if (Mathf.Abs(leftStickInput.x) > Mathf.Abs(leftStickInput.y))
                {
                    if (leftStickInput.x > 0)
                        direction = DIRECTION.RIGHT;
                    else
                        direction = DIRECTION.LEFT;
                }
                else
                {
                    if (leftStickInput.y > 0)
                    {
                        direction = DIRECTION.UP;
                    }
                    else
                    {
                        direction = DIRECTION.DOWN;
                    }
                }
            }
        }
        else
        {
            animator.SetBool("canRun", false);
        }

        animator.SetInteger("direction", (int)direction);
    }
}