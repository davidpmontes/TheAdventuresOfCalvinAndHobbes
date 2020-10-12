using System.Collections;
using UnityEngine;

public enum DIRECTION
{
    LEFT,
    RIGHT,
    DOWN,
    UP
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private GameObject balloonPrefab;

    private Vector2 leftStickInput;
    private bool runInput;

    private UnityEngine.InputSystem.PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private DIRECTION direction = DIRECTION.DOWN;
    private MovingObject movingObject;

    public UnityEngine.InputSystem.InputAction fireAction;

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
        StartCoroutine(Throw());
    }

    private void FlipX()
    {
        if (direction == DIRECTION.LEFT || direction == DIRECTION.RIGHT)
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
        else
        {
            animator.SetBool("canRun", false);
        }

        animator.SetInteger("direction", (int)direction);
    }

    public IEnumerator Throw()
    {
        yield return new WaitForSeconds(0.2f);
        var balloon = Instantiate(balloonPrefab, null);
        balloon.GetComponent<Balloon>().Init(transform.position, movingObject.GetVelocity() * speed, direction);
    }
}
