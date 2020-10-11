using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum DIRECTION
    {
        LEFT_RIGHT,
        DOWN,
        UP
    }

    private Vector2 leftStickInput;
    private bool runInput;

    private UnityEngine.InputSystem.PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private DIRECTION direction = DIRECTION.DOWN;
    private MovingObject movingObject;

    public UnityEngine.InputSystem.InputAction fireAction;

    [SerializeField] private MovingObjectConfig config = default;
    [SerializeField] private float moveSpeed = default;

    private void Awake()
    {
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
        float speed = runInput ? config.runningSpeed : config.walkingSpeed;

        movingObject.CalculateVelocity(leftStickInput);
        movingObject.Move(speed);
    }

    private void GetInput()
    {
        leftStickInput = playerInput.actions["Move"].ReadValue<Vector2>();
        runInput = playerInput.actions["Run"].ReadValue<float>() > 0;
    }

    public void OnFire()
    {
        animator.SetTrigger("fire");
    }

    private void FlipX()
    {
        if (direction == DIRECTION.LEFT_RIGHT)
        {
            if (Mathf.Abs(leftStickInput.x) > 0.1f)
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

            if (Mathf.Abs(leftStickInput.x) > Mathf.Abs(leftStickInput.y))
            {
                direction = DIRECTION.LEFT_RIGHT;
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
        else
        {
            animator.SetBool("canRun", false);
        }

        animator.SetInteger("direction", (int)direction);
    }
}
