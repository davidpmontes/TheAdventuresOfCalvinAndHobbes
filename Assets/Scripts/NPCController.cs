using UnityEngine;

public class NPCController : MonoBehaviour
{
    private Vector2 leftStickInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    protected DIRECTION direction = DIRECTION.DOWN;
    protected MovingObject movingObject;

    private float moveInterval = 1f;
    private float timeOfLastMove;
    private bool justMoved;

    [SerializeField] private MovingObjectConfig config = default;
    private float speed;

    private void Awake()
    {
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
        movingObject.Move(leftStickInput, speed);
    }

    private void GetInput()
    {
        if (timeOfLastMove < Time.time)
        {
            timeOfLastMove = Time.time + moveInterval;

            if (justMoved)
            {
                leftStickInput = Vector2.zero;
                animator.SetBool("isIdle", true);
            }
            else
            {
                DIRECTION randDir = (DIRECTION)Random.Range(0, 4);
                if (Random.value < .25f)
                {
                    speed = 0;
                    animator.SetBool("isIdle", true);
                }
                else
                {
                    speed = config.walkingSpeed;
                    animator.SetBool("isIdle", false);
                }
                animator.SetInteger("direction", (int)randDir);

                switch (randDir)
                {
                    case DIRECTION.LEFT:
                        leftStickInput = new Vector2(-1, 0);
                        break;
                    case DIRECTION.RIGHT:
                        leftStickInput = new Vector2(1, 0);
                        break;
                    case DIRECTION.UP:
                        leftStickInput = new Vector2(0, 1);
                        break;
                    case DIRECTION.DOWN:
                        leftStickInput = new Vector2(0, -1);
                        break;
                }
            }

            justMoved = !justMoved;
        }
    }

    private void FlipX()
    {
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
}
