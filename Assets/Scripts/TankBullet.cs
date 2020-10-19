using System.Collections;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    private float lifespan = 10;
    private float speed = 10;
    private Rigidbody2D rb;

    [SerializeField] private Sprite Degree00 = default;
    [SerializeField] private Sprite Degree22 = default;
    [SerializeField] private Sprite Degree45 = default;

    [SerializeField] private GameObject bulletTrailPrefab = default;

    public LayerMask layerMask;
    private float angle = 90f;

    public void Init(Vector2 pos)
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = pos;
        StopAllCoroutines();
        StartCoroutine(DrawTrail());
        StartCoroutine(Destroy());
        rb.velocity = Vector2.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<CircleCollider2D>().enabled = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 100, layerMask);

        if (hit)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            rb.velocity = hit.normal * speed;
            angle = Mathf.Abs(Mathf.Atan2(hit.normal.x, hit.normal.y) * Mathf.Rad2Deg);

            var adjustedAngle = angle - 90;                        
            if (adjustedAngle > 0) GetComponent<SpriteRenderer>().flipY = true;

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
        }
    }

    private IEnumerator DrawTrail()
    {
        while(true)
        {
            var trail = Instantiate(bulletTrailPrefab);
            trail.GetComponent<BulletTrail>().Init(transform.position, angle);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}
