using System.Collections;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private float lifespan = 0.5f;

    [SerializeField] private Sprite Degree00 = default;
    [SerializeField] private Sprite Degree22 = default;
    [SerializeField] private Sprite Degree45 = default;

    public void Init(Vector2 pos, float angle)
    {
        var adjustedAngle = angle - 90;
        if (adjustedAngle > 0) GetComponent<SpriteRenderer>().flipX = true;
        if (Mathf.Abs(adjustedAngle) < 11.25f)
        {
            GetComponent<SpriteRenderer>().sprite = Degree00;
        }
        else if (Mathf.Abs(adjustedAngle) < 33.75f)
        {
            GetComponent<SpriteRenderer>().sprite = Degree22;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = Degree45;
        }

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
