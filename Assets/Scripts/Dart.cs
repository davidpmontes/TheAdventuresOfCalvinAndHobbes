using System.Collections;
using UnityEngine;

public class Dart : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites = default;
    private float duration = 2f;
    private float speed = 35f;
    private Vector2 initVel;

    public void Init(Vector2 pos, Vector2 vel, DIRECTION dir)
    {
        switch (dir)
        {
            case DIRECTION.LEFT:
                transform.position = pos + new Vector2(-2f, 2.8f);
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[1];
                initVel = new Vector2(vel.x, 0);
                StartCoroutine(Shoot(new Vector2(-1, 0)));
                break;
            case DIRECTION.RIGHT:
                transform.position = pos + new Vector2(2f, 2.8f);
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[0];
                initVel = new Vector2(vel.x, 0);
                StartCoroutine(Shoot(new Vector2(1, 0)));
                break;
            case DIRECTION.UP:
                transform.position = pos + new Vector2(-0.1f, 5);
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[3];
                initVel = new Vector2(0, vel.y);
                StartCoroutine(Shoot(new Vector2(0, 1)));
                break;
            case DIRECTION.DOWN:
                transform.position = pos + new Vector2(-0.1f, 1.5f);
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[2];
                initVel = new Vector2(0, vel.y);
                StartCoroutine(Shoot(new Vector2(0, -1)));
                break;
        }
    }

    private IEnumerator Shoot(Vector2 dir)
    {
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            transform.Translate(initVel + dir * speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }
}
