using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    //属性值
    public float moveSpeed = 3;
    private Vector3 bullectEulerAngles;
    private float timeVal;
    private float defendTimeVal = 3;
    private bool isDefended = true;

    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//上右下左
    public GameObject bullectPrefab;
    public GameObject explosionPrefab;
    public GameObject defendPrefab;
    public AudioSource moveAudio;
    public AudioClip[] tankAudio;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isDefended)
        {
            defendPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0)
            {
                isDefended = false;
                defendPrefab.SetActive(false);
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (PlayerManager.Instance.isDefeat)
        {
            return;
        }
        Move();

        if (timeVal > 0.4f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }

    }

    //坦克攻击
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullectPrefab, transform.position,Quaternion.Euler(transform.eulerAngles+bullectEulerAngles));
            timeVal = 0;
        }

    }

    //坦克移动
    private void Move()
    {
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = tankSprite[2];
            bullectEulerAngles = new Vector3(0, 0, 180);

        }
        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bullectEulerAngles = new Vector3(0, 0, 0);
        }

        if (Mathf.Abs(v) > 0.05f)
        {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }

        if (v != 0)
        {
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            sr.sprite = tankSprite[3];
            bullectEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            sr.sprite = tankSprite[1];
            bullectEulerAngles = new Vector3(0, 0, -90);
        }
        if (Mathf.Abs(h) > 0.05f)
        {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else
        {
            moveAudio.clip = tankAudio[0];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
    }

    //坦克死亡
    private void Die()
    {
        if (isDefended)
        {
            return;
        }
        PlayerManager.Instance.isDead = true;
        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
    }
}
