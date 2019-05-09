using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyBase
{
    // Use this for initialization
    private void Start()
    {
        score = 150;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        Attack();
    }

    public override void Attack()
    {
        if (Mathf.Abs(transform.position.y - Player.instance.transform.position.y) < 1f)
            if (Time.time - oTime > shootDelay)
            {
                bullet.transform.position = transform.position;
                Instantiate(bullet);
                oTime = Time.time;
            }
    }

    public override void Move()
    {
        transform.Translate(GetDir() * Time.deltaTime * speed);
    }

    private Vector2 GetDir()
    {
        Vector2 dir = new Vector2(0, Player.instance.transform.position.y - transform.position.y);
        return dir;
    }
}