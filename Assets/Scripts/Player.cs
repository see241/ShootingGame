using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Weapon
{
    Normal, Quad, Chase
}

public delegate void Shooting();

public class Player : MonoBehaviour
{
    public static Player instance;
    public float speed;
    public float shootDelay;

    public Transform upShootPos;
    public Transform downShootPos;
    public Transform chaseShootPos;

    public GameObject ShootingParts1;
    public GameObject ShootingParts2;

    public GameObject bullet;
    private bool isSlow;
    private int shootCount;
    public int nextPhase;

    public int sc;

    public int scoreCount
    {
        get { return sc; }
        set
        {
            sc = value;
            progressBar.fillAmount = scoreCount / (float)nextPhase;
            if (sc > nextPhase) { sc = 0; EnemyManager.instance.Phase++; }
        }
    }

    public ParticleSystem destroyParticle;
    public ParticleSystem nuclearParticle;

    public bool nuclearEnable;

    #region QuadShoot

    private bool qs;

    public bool quadShoot
    {
        get { return qs; }
        set
        {
            qs = value;
            curQuadTimer = maxQuadTimer;
            ShootingParts1.SetActive(qs);
            ShootingParts2.SetActive(qs);
            quadIcon.enabled = qs;
        }
    }

    private float curQuadTimer;
    public float maxQuadTimer;

    #endregion QuadShoot

    #region ChaseShoot

    private bool cs;

    public bool chaseShoot
    {
        get { return cs; }
        set
        {
            cs = value;
            curChaseTimer = maxChaseTimer;
            bullet.GetComponent<Bullet>().SetChase(cs);
            chaseIcon.enabled = cs;
        }
    }

    private float curChaseTimer;
    public float maxChaseTimer;

    #endregion ChaseShoot

    #region UI

    public Image chaseBar;
    public Image quadBar;
    public Image chaseIcon;
    public Image quadIcon;
    public Image nuclearIcon;
    public Image progressBar;

    #endregion UI

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(ShootCoroutine());
        chaseShoot = true;
        quadShoot = true;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && nuclearEnable)
        {
            EnemyManager.instance.Nuclear(nuclearParticle);
            nuclearEnable = false;
            nuclearIcon.enabled = false;
        }
        if (chaseShoot) ChaseTimer();
        if (quadShoot) QuadTimer();
    }

    private void Move()
    {
        Vector2 vct = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            if (transform.position.y < 5.5f)
                vct.y = 0.75f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (transform.position.y > -3.3f)
                vct.y = -0.75f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > -7.2)
                vct.x = -0.65f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < 7.5)
                vct.x = 1;
        }
        transform.Translate(vct * speed * Time.deltaTime);
    }

    #region ShootingArea

    private void NormalShooting()
    {
        if (shootCount % 2 == 0)
        {
            bullet.transform.position = upShootPos.position;
            Instantiate(bullet);
        }
        else
        {
            bullet.transform.position = downShootPos.position;
            Instantiate(bullet);
        }
    }

    private void TripleShooting()
    {
        bullet.transform.position = ShootingParts1.transform.position;
        Instantiate(bullet);
        bullet.transform.position = ShootingParts2.transform.position;
        Instantiate(bullet);
    }

    private void ChaseShooting()
    {
        bullet.transform.position = chaseShootPos.position;
        Instantiate(bullet);
    }

    #endregion ShootingArea

    private void QuadTimer()
    {
        curQuadTimer -= Time.deltaTime;
        quadBar.fillAmount = curQuadTimer / maxQuadTimer;
        if (curQuadTimer < 0) quadShoot = false;
    }

    private void ChaseTimer()
    {
        curChaseTimer -= Time.deltaTime;
        chaseBar.fillAmount = curChaseTimer / maxChaseTimer;
        if (curChaseTimer < 0) chaseShoot = false;
    }

    private IEnumerator ShootCoroutine()
    {
        float cTime = Time.time;
        while (true)
        {
            NormalShooting();
            if (chaseShoot) ChaseShooting();
            if (quadShoot) TripleShooting();
            shootCount++;
            yield return new WaitForSeconds(shootDelay);
        }
    }

    private void TimeSlow()
    {
        isSlow = true;
        speed /= 0.5f;
        Time.timeScale = 0.25f;
    }

    private void TimeReturn()
    {
        Time.timeScale = 1f;
        speed *= 0.5f;
        isSlow = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet")
        {
            destroyParticle.startColor = new Color32(255, 0, 92, 255);
            ParticleSystem ptc = Instantiate(destroyParticle);
            ptc.transform.position = transform.position;
            Destroy(ptc.gameObject, ptc.duration + ptc.startLifetime);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (other.tag == "Item")
        {
            switch (other.GetComponent<Item>().state)
            {
                case ItemState.QuadShooting:
                    quadShoot = true;
                    break;

                case ItemState.ChaseShooting:
                    chaseShoot = true;
                    break;

                case ItemState.Nuclear:
                    nuclearEnable = true;
                    nuclearIcon.enabled = true;
                    break;
            }
            Destroy(other.gameObject);
        }
    }
}