using System.Collections;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] private GameObject waterExplodePrefab = default;
    private float throwDuration = 0.5f;
    private float throwSpeed = 20f;
    private Vector2 initVel;

    public void Init(Vector2 pos, Vector2 vel, DIRECTION dir)
    {
        Debug.Log("init: " + vel);
        initVel = vel;

        switch(dir)
        {
            case DIRECTION.LEFT:
                transform.position = pos + new Vector2(1, 4);
                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 180);
                StartCoroutine(ThrowLeft());
                break;
            case DIRECTION.RIGHT:
                transform.position = pos + new Vector2(-1, 4);
                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(ThrowRight());
                break;
            case DIRECTION.UP:
                transform.position = pos + new Vector2(1, 5);
                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 90);
                StartCoroutine(ThrowUp());
                break;
            case DIRECTION.DOWN:
                transform.position = pos + new Vector2(-1, 3);
                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 270);
                StartCoroutine(ThrowDown());
                break;
        }
    }

    private IEnumerator ThrowLeft()
    {
        float endTime = Time.time + throwDuration;
        Vector2 dir = new Vector2(-1, 1);

        while(Time.time < endTime)
        {
            transform.Translate(initVel + dir * throwSpeed * Time.deltaTime);
            dir.y -= 5.5f * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(WaterExplode());
    }

    private IEnumerator ThrowRight()
    {
        float endTime = Time.time + throwDuration;
        Vector2 dir = new Vector2(1, 1);

        while (Time.time < endTime)
        {
            transform.Translate(initVel + dir * throwSpeed * Time.deltaTime);
            dir.y -= 5.5f * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(WaterExplode());
    }

    private IEnumerator ThrowUp()
    {
        float endTime = Time.time + throwDuration;
        Vector2 dir = new Vector2(0, 1);

        while (Time.time < endTime)
        {
            transform.Translate(initVel + dir * throwSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(WaterExplode());
    }

    private IEnumerator ThrowDown()
    {
        float endTime = Time.time + throwDuration;
        Vector2 dir = new Vector2(0, -1);

        while (Time.time < endTime)
        {
            transform.Translate(initVel + dir * throwSpeed * Time.deltaTime);
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
