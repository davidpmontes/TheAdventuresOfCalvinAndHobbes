using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    private float lifespan = 3;
    private float speed = 10;

    [SerializeField] private Sprite DegreeNeg45;
    [SerializeField] private Sprite DegreeNeg22;
    [SerializeField] private Sprite Degree0;
    [SerializeField] private Sprite DegreePos22;
    [SerializeField] private Sprite DegreePos45;

    [SerializeField] private GameObject bulletTrailPrefab;

    private Queue<GameObject> trails = new Queue<GameObject>();

    public void Init(Vector2 pos)
    {
        transform.position = pos;
        StopAllCoroutines();
        StartCoroutine(DrawTrail());
        StartCoroutine(Destroy());
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private IEnumerator DrawTrail()
    {
        while(true)
        {
            var trail = Instantiate(bulletTrailPrefab);
            trail.GetComponent<BulletTrail>().Init(transform.position);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}
