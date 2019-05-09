using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
    public static EnemyFinder instance;

    public GameObject target;

    public GameObject marker;

    private bool duringFinding;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Player.instance.chaseShoot)
            if (target == null && !duringFinding)
                StartCoroutine(SearchStart());
    }

    private IEnumerator SearchStart()
    {
        duringFinding = true;
        while (transform.position.x < 9.5)
        {
            transform.Translate(1f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        GetComponent<TrailRenderer>().enabled = false;
        transform.position = new Vector2(-10, 1);
        yield return new WaitForSeconds(1f);
        GetComponent<TrailRenderer>().enabled = true;
        duringFinding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target == null)
        {
            if (other.tag == "Enemy")
            {
                target = other.gameObject;
                GameObject go = Instantiate(marker, other.transform);
                go.transform.position = other.transform.position + new Vector3(0, 0, 1);
            }
        }
    }
}