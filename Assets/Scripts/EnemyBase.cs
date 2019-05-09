using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public int hp;
    public float speed;
    public float shootDelay;
    public GameObject bullet;
    protected float oTime;
    public ParticleSystem particle;
    public GameObject item;
    protected int score;

    public abstract void Move();

    public abstract void Attack();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            WhenDestroy();
            Destroy(other.gameObject);
            Player.instance.scoreCount += score;
            Destroy(gameObject);
        }
    }

    private void WhenDestroy()
    {
        particle.startColor = GetComponent<MeshRenderer>().material.color;
        ParticleSystem ptc = Instantiate(particle);
        ptc.transform.position = transform.position;
        Destroy(ptc.gameObject, ptc.duration + ptc.startLifetime);
        int r = Random.Range(0, 100);
        if (r < 10)
        {
            item.transform.position = transform.position;
            Instantiate(item);
        }
    }
}