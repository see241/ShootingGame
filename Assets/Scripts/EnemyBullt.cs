using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullt : MonoBehaviour
{
    public float speed;

    // Use this for initialization
    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
    }
}