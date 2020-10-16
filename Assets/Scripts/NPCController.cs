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

    protected const string IDLE_UP = "IdleUp";
    protected const string IDLE_DOWN = "IdleDown";
    protected const string IDLE_RIGHT = "IdleRight";
    protected const string WALK_UP = "WalkUp";
    protected const string WALK_DOWN = "WalkDown";
    protected const string WALK_RIGHT = "WalkRight";

    [SerializeField] private MovingObjectConfig config = default;
    private float speed;

    private string currentState;
    protected bool isIdle;


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
        //UpdateAnimator();
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
                isIdle = true;
            }
            else
            {
                DIRECTION randDir = (DIRECTION)Random.Range(0, 4);
                if (Random.value < .25f)
                {
                    speed = 0;
                    isIdle = true;
                }
                else
                {
                    speed = config.walkingSpeed;
                    isIdle = false;
                }
                direction = randDir;

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
            AnimatorDirection();
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

    protected void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    virtual public void AnimatorDirection()
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
