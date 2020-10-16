using System.Collections;
using UnityEngine;

public class CalvinController : PlayerController
{
    [SerializeField] private GameObject balloonPrefab = default;

    public override void OnAttack()
    {
        if (isAttacking) return;
        isAttacking = true;
        switch (direction)
        {
            case DIRECTION.UP:
                ChangeAnimationState(ATTACK_UP);
                break;
            case DIRECTION.DOWN:
                ChangeAnimationState(ATTACK_DOWN);
                break;
            case DIRECTION.LEFT:
            case DIRECTION.RIGHT:
                ChangeAnimationState(ATTACK_RIGHT);
                break;
        }
        StartCoroutine(Throw());
    }

    private IEnumerator Throw()
    {
        yield return new WaitForSeconds(0.2f);
        var balloon = Instantiate(balloonPrefab, null);
        balloon.GetComponent<Balloon>().Init(transform.position, movingObject.GetAdjustedVelocity(), direction);
        yield return new WaitForSeconds(0.233f);
        isAttacking = false;
    }
}
