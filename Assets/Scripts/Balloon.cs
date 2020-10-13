using System.Collections;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] private GameObject waterExplodePrefab = default;
    private float duration = 0.5f;
    private float speed = 20f;
    private Vector2 initVel;

    public void Init(Vector2 pos, Vector2 vel, DIRECTION dir)
    {
        switch(dir)
        {
            case DIRECTION.LEFT:
                transform.position = pos + new Vector2(1, 4);
                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 180);
                initVel = new Vector2(vel.x, 0);
                StartCoroutine(Throw(new Vector2(-1, 1), -5.5f));
                break;
            case DIRECTION.RIGHT:
                transform.position = pos + new Vector2(-1, 4);
                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                initVel = new Vector2(vel.x, 0);
                StartCoroutine(Throw(new Vector2(1, 1), -5.5f));
                break;
            case DIRECTION.UP:
                transform.position = pos + new Vector2(1, 5);
                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 90);
                initVel = new Vector2(0, vel.y);
                StartCoroutine(Throw(new Vector2(0, 1), 0f));
                break;
            case DIRECTION.DOWN:
                transform.position = pos + new Vector2(-1, 3);
                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 270);
                initVel = new Vector2(0, vel.y);
                StartCoroutine(Throw(new Vector2(0, -1), 0f));
                break;
        }
    }

    private IEnumerator Throw(Vector2 dir, float yDir)
    {
        float endTime = Time.time + duration;

        while(Time.time < endTime)
        {
            transform.Translate(initVel + dir * speed * Time.deltaTime);
            dir.y += yDir * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(WaterExplode());
    }

    private IEnumerator WaterExplode()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        var waterExplode = Instantiate(waterExplodePrefab, null);
        waterExplode.transform.position = transform.position;
        yield return new WaitForSeconds(waterExplode.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length);
        Destroy(waterExplode);
    }
}
