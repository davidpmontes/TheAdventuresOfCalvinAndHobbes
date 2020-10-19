using System.Collections;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private float lifespan = 0.5f;

    [SerializeField] private Sprite Degree00;
    [SerializeField] private Sprite Degree22;
    [SerializeField] private Sprite Degree45;
    [SerializeField] private Sprite Degree67;
    [SerializeField] private Sprite Degree90;

    public void Init(Vector2 pos, float angle)
    {
        var adjustedAngle = angle - 90;
        Debug.Log(adjustedAngle);
        if (adjustedAngle > 0) GetComponent<SpriteRenderer>().flipX = true;
        if (Mathf.Abs(adjustedAngle) < 11.25f)
        {
            GetComponent<SpriteRenderer>().sprite = Degree00;
        }
        else if (Mathf.Abs(adjustedAngle) < 33.75f)
        {
            GetComponent<SpriteRenderer>().sprite = Degree22;
        }
        else if (Mathf.Abs(adjustedAngle) < 56.25f)
        {
            GetComponent<SpriteRenderer>().sprite = Degree45;
        }
        else if (Mathf.Abs(adjustedAngle) < 78.75f)
        {
            GetComponent<SpriteRenderer>().sprite = Degree67;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = Degree90;
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
