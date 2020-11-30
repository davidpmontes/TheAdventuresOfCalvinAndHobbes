using System.Collections;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public static TankController Instance { get; private set; }

    [SerializeField] private GameObject bulletPrefab = default;
    private GameObject sprites;
    private GameObject barrel;
    private GameObject blast;

    protected bool canRun;
    protected bool isIdle;
    protected bool isAttacking;

    private Vector2 leftStickInput;
    private bool runInput;
    private bool aimLockInput;

    private UnityEngine.InputSystem.PlayerInput playerInput;

    protected Animator animator;
    protected DIRECTION direction = DIRECTION.DOWN;
    protected MovingObject movingObject;

    private Vector2 jitteryLocation;
    private string currentState;

    private float speed = 5f;
    private Coroutine showhideBlast;

    private void Awake()
    {
        Instance = this;
        jitteryLocation = transform.position;
        sprites = Utils.FindChildByNameRecursively(transform, "Sprites");
        barrel = Utils.FindChildByNameRecursively(transform, "barrel");
        blast = Utils.FindChildByNameRecursively(transform, "blast");
        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        animator = GetComponent<Animator>();
        movingObject = GetComponent<MovingObject>();
        blast.SetActive(false);
    }

    void Update()
    {
        GetInput();
        FlipX();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        movingObject.MoveJittery(leftStickInput, speed, ref jitteryLocation, sprites);
    }

    public void OnMove(UnityEngine.InputSystem.InputValue value)
    {
        leftStickInput = value.Get<Vector2>();
    }

    public void OnAttack()
    {
        if (showhideBlast != null) StopCoroutine(showhideBlast);
        showhideBlast = StartCoroutine(ShowHideBlast());
        var bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<TankBullet>().Init(barrel.transform.position);
    }

    private IEnumerator ShowHideBlast()
    {
        blast.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        blast.SetActive(false);
    }

    private void GetInput()
    {
        runInput = playerInput.actions["Run"].ReadValue<float>() > 0;
        aimLockInput = playerInput.actions["AimLock"].ReadValue<float>() > 0;
    }
        
    private void FlipX()
    {
        if (aimLockInput) return;

        if (direction == DIRECTION.LEFT || direction == DIRECTION.RIGHT)
        {
            if (Mathf.Abs(leftStickInput.x) != 0.0f)
            {
                transform.localScale = new Vector3(leftStickInput.x < 0 ? -1 : 1, 1, 1);
            }
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    private void UpdateAnimator()
    {
        
    }

    protected void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    virtual public void AnimatorDirection()
    {
        
    }
}