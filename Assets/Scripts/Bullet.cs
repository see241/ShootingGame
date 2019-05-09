using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public bool isChase;

    // Use this for initialization
    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isChase)
            ChaseShooting();
        NormalShooting();
    }

    private void NormalShooting()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    public void ChaseShooting()
    {
        if (EnemyFinder.instance.target != null)
        {
            float ang = GetAngle(transform.position, EnemyFinder.instance.target.transform.position);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, (ang + -90)));
        }
    }

    public float RadianToDegree(float rad)
    {
        return rad * (180 / 3.1415f);
    }

    public float GetAngle(Vector2 p1, Vector2 p2)
    {
        float ang = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI;
        if (ang < 0) ang += 360;
        return ang;
    }

    public void SetChase(bool flag)
    {
        isChase = flag;
    }
}