using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum WALK_TYPE
    {
        LEFT_RIGHT,
        DOWN,
        UP
    }

    private Vector2 leftStickInput;
    private UnityEngine.InputSystem.PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private WALK_TYPE walkType;
    private MovingObject movingObject;

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
        movingObject.CalculateVelocity(leftStickInput);
        movingObject.Move();
    }

    private void GetInput()
    {
        leftStickInput = playerInput.actions["Move"].ReadValue<Vector2>();
    }

    private void FlipX()
    {

        if (walkType == WALK_TYPE.LEFT_RIGHT)
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
        if (leftStickInput.magnitude > 0)
            animator.speed = 1;
        else
            animator.speed = 0;

        if (leftStickInput.magnitude == 0)
            return;

        if (Mathf.Abs(leftStickInput.x) > Mathf.Abs(leftStickInput.y))
        {
            walkType = WALK_TYPE.LEFT_RIGHT;
        }
        else
        {
            if (leftStickInput.y > 0)
            {
                walkType = WALK_TYPE.UP;
            }
            else
            {
                walkType = WALK_TYPE.DOWN;
            }
        }

        animator.SetInteger("walkType", (int)walkType);
    }
}
