using UnityEngine;

public class CombatCalvinController : PlayerController
{
    [SerializeField] private GameObject dartPrefab = default;

    public override void OnAttack()
    {
        base.OnAttack();
        Shoot();
    }

    private void Shoot()
    {
        var dart = Instantiate(dartPrefab, null);
        dart.GetComponent<Dart>().Init(transform.position, movingObject.GetAdjustedVelocity(), direction);
    }

    public override void AnimatorAttack()
    {
    }
}
