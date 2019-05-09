using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemState
{
    QuadShooting, ChaseShooting, Nuclear
}

public class Item : MonoBehaviour
{
    private Vector2 moveVec;
    public float speed;
    public ItemState state;
    private Color[] colors = new Color[3] { Color.cyan, Color.green, Color.red };

    // Use this for initialization
    private void Start()
    {
        state = (ItemState)Random.Range(0, 3);
        GetComponent<MeshRenderer>().material.color = colors[(int)state];
        moveVec = new Vector2(45, 45);
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position.y > 5.5f)
        {
            float vX = Random.Range(-1.0f, 1.0f);
            float vY = Random.Range(-1.0f, 0f);
            moveVec = new Vector2(vX, vY);
        }
        if (transform.position.y < -3.3f)
        {
            float vX = Random.Range(-1.0f, 1.0f);
            float vY = Random.Range(0f, 1.0f);
            moveVec = new Vector2(vX, vY);
        }
        if (transform.position.x < -7.2)
        {
            float vX = Random.Range(0f, 1.0f);
            float vY = Random.Range(-1.0f, 1.0f);
            moveVec = new Vector2(vX, vY);
        }
        if (transform.position.x > 7.5)
        {
            float vX = Random.Range(-1.0f, 0f);
            float vY = Random.Range(-1.0f, 1.0f);
            moveVec = new Vector2(vX, vY);
        }
        transform.Translate(moveVec.normalized * Time.deltaTime * speed);
    }
}