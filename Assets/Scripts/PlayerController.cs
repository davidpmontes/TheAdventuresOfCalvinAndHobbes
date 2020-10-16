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

    protected const string IDLE_UP = "IdleUp";
    protected const string IDLE_DOWN = "IdleDown";
    protected const string IDLE_RIGHT = "IdleRight";
    protected const string WALK_UP = "WalkUp";
    protected const string WALK_DOWN = "WalkDown";
    protected const string WALK_RIGHT = "WalkRight";
    protected const string RUN_UP = "RunUp";
    protected const string RUN_DOWN = "RunDown";
    protected const string RUN_RIGHT = "RunRight";
    protected const string ATTACK_UP = "AttackUp";
    protected const string ATTACK_DOWN = "AttackDown";
    protected const string ATTACK_RIGHT = "AttackRight";
    protected bool canRun;
    protected bool isIdle;
    protected bool isAttacking;

    private Vector2 leftStickInput;
    private bool runInput;
    private bool aimLockInput;

    private UnityEngine.InputSystem.PlayerInput playerInput;

    private SpriteRenderer spriteRenderer;
    
    protected Animator animator;
    protected DIRECTION direction = DIRECTION.DOWN;
    protected MovingObject movingObject;

    private string currentState;

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

    public abstract void OnAttack();

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
        if (leftStickInput.magnitude > 0)
        {
            isIdle = false;
            canRun = runInput;

            if (!aimLockInput)
            {
                if (Mathf.Abs(leftStickInput.x) > Mathf.Abs(leftStickInput.y))
                {
                    if (leftStickInput.x > 0)
                    {
                        direction = DIRECTION.RIGHT;
                    }
                    else
                    {
                        direction = DIRECTION.LEFT;
                    }
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
            isIdle = true;
            canRun = false;
        }

        AnimatorDirection();
    }

    protected void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    virtual public void AnimatorDirection()
    {
        if (isAttacking) return;

        if (canRun)
        {
            switch (direction)
            {
                case DIRECTION.UP:
                    ChangeAnimationState(RUN_UP);
                    break;
                case DIRECTION.DOWN:
                    ChangeAnimationState(RUN_DOWN);
                    break;
                case DIRECTION.LEFT:
                case DIRECTION.RIGHT:
                    ChangeAnimationState(RUN_RIGHT);
                    break;
            }
        }
        else
        {
            if (isIdle)
            {
                switch (direction)
                {
                    case DIRECTION.UP:
                        ChangeAnimationState(IDLE_UP);
                        break;
                    case DIRECTION.DOWN:
                        ChangeAnimationState(IDLE_DOWN);
                        break;
                    case DIRECTION.LEFT:
                    case DIRECTION.RIGHT:
                        ChangeAnimationState(IDLE_RIGHT);
                        break;
                }
            }    
            else
            {
                switch (direction)
                {
                    case DIRECTION.UP:
                        ChangeAnimationState(WALK_UP);
                        break;
                    case DIRECTION.DOWN:
                        ChangeAnimationState(WALK_DOWN);
                        break;
                    case DIRECTION.LEFT:
                    case DIRECTION.RIGHT:
                        ChangeAnimationState(WALK_RIGHT);
                        break;
                }
            }
        }
    }
}