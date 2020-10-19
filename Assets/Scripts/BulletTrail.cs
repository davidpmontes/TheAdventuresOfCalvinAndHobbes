using System.Collections;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private float lifespan = 1f;

    public void Init(Vector2 pos)
    {
        StopAllCoroutines();
        StartCoroutine(Destroy());
        transform.position = pos;
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}
