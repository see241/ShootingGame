using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public GameObject enemy1;
    public int Phase;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (transform.GetChildCount() <= 0)
            {
                StartCoroutine(SpawnEnemy());
            }
            yield return null;
        }
    }

    private IEnumerator SpawnEnemy()
    {
        switch (Phase)
        {
            case 0:
                for (int i = 0; i < 3; i++)
                {
                    GameObject go = Instantiate(enemy1, transform);
                    StartCoroutine(GoToSpawnPosCoroutine(go, i));
                    yield return null;
                }
                break;

            case 1:

                break;

            case 2:

                break;
        }
    }

    public void Nuclear(ParticleSystem pt)
    {
        StartCoroutine(KillAll(pt));
    }

    private IEnumerator KillAll(ParticleSystem ptc)
    {
        for (int i = 0; i < transform.GetChildCount(); i++)
        {
            ParticleSystem particle = Instantiate(ptc);
            particle.transform.position = transform.GetChild(i).transform.position;
            Destroy(transform.GetChild(i).gameObject, 0.1f);
            yield return null;
        }
    }

    private IEnumerator GoToSpawnPosCoroutine(GameObject go, int ind)
    {
        go.SetActive(false);
        for (int i = 0; i < 500; i++)
        {
            go.transform.position = Vector2.Lerp(go.transform.position, new Vector2(10 - 2 - ind * 1.5f, go.transform.position.y), 0.1f);
            if (go.transform.position.x < 10) go.SetActive(true);
            yield return new WaitForSeconds(0.01f);
        }
        go.transform.position = new Vector2(10 - 2 - ind * 1.5f, go.transform.position.y);
    }
}