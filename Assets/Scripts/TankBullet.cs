using System.Collections;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    private float lifespan = 10;
    private float speed = 10;
    private Rigidbody2D rb;

    [SerializeField] private Sprite DegreeNeg45;
    [SerializeField] private Sprite DegreeNeg22;
    [SerializeField] private Sprite Degree0;
    [SerializeField] private Sprite DegreePos22;
    [SerializeField] private Sprite DegreePos45;

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
            rb.velocity = hit.normal * speed;
            angle = Mathf.Abs(Mathf.Atan2(hit.normal.x, hit.normal.y) * Mathf.Rad2Deg);
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
