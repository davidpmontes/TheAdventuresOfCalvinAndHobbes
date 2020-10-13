using System.Collections;
using UnityEngine;

public class CalvinController : PlayerController
{
    [SerializeField] private GameObject balloonPrefab = default;

    public override void OnAttack()
    {
        base.OnAttack();
        StartCoroutine(Throw());
    }

    private IEnumerator Throw()
    {
        yield return new WaitForSeconds(0.2f);
        var balloon = Instantiate(balloonPrefab, null);
        balloon.GetComponent<Balloon>().Init(transform.position, movingObject.GetAdjustedVelocity(), direction);
    }
}
