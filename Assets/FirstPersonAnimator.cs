using UnityEngine;

public class FirstPersonAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var leftInput = FirstPersonController.Instance.GetLeftInput().magnitude;
        var facingDirection = FirstPersonController.Instance.GetFacingDirection();

        if (Mathf.Approximately(leftInput, 0))
        {
            if (facingDirection.Equals("up"))
                animator.Play("IdleUp");
            else if (facingDirection.Equals("down"))
                animator.Play("IdleDown");
            else
            {
                if (facingDirection.Equals("left"))
                    spriteRenderer.flipX = true;
                else
                    spriteRenderer.flipX = false;

                animator.Play("IdleRight");
            }
        }
        else
        {
            if (leftInput < 0.5f)
            {
                if (facingDirection.Equals("up"))
                    animator.Play("WalkUp");
                else if (facingDirection.Equals("down"))
                    animator.Play("WalkDown");
                else
                {
                    if (facingDirection.Equals("left"))
                        spriteRenderer.flipX = true;
                    else
                        spriteRenderer.flipX = false;

                    animator.Play("WalkRight");
                }
            }
            else
            {
                if (facingDirection.Equals("up"))
                    animator.Play("RunUp");
                else if (facingDirection.Equals("down"))
                    animator.Play("RunDown");
                else
                {
                    if (facingDirection.Equals("left"))
                        spriteRenderer.flipX = true;
                    else
                        spriteRenderer.flipX = false;

                    animator.Play("RunRight");
                }
            }
        }
    }
}
