using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Hamster : MonoBehaviour
{
    /// <summary>弾</summary>
    [SerializeField] GameObject m_bulletPrefab = null;
    /// <summary>弾を出す場所</summary>
    [SerializeField] Transform m_muzzle;
    /// <summary>移動速度</summary>
    [SerializeField] float m_moveSpeed = 0;
    /// <summary>弾の速度</summary>
    [SerializeField] float m_bulletSpeed = 0;
    /// <summary>発射間隔 ( 時間 )</summary>
    [SerializeField] float m_fireDelayTime = 0;
    /// <summary>体力</summary>
    [SerializeField] int m_hp;
    /// <summary>ゲーム中かのフラグ</summary>
    [SerializeField] bool m_isStarted = false;
    /// <summary>リザルト画面</summary>
    [SerializeField] GameObject m_resultWindow = null;
    /// <summary>被弾時の効果音</summary>
    [SerializeField] AudioClip m_damagedSE;

    bool m_isFire = false;
    GameManager m_gm;
    AudioSource m_souce;

    public bool IsStarted 
    {
        get => m_isStarted; 
        set { m_isStarted = value; }
    }

    InputAction m_move, m_dir;
    Rigidbody2D m_rb;

    private void Awake()
    {
        m_move = GetComponent<PlayerInput>().currentActionMap["Move"];
        m_dir = GetComponent<PlayerInput>().currentActionMap["Direction"];
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        m_gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_souce = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (m_isStarted)
        {
            Move();
            Direction();
            if (!m_isFire)
            {
                StartCoroutine(Fire());
                m_isFire = true;
            }
        }
    }

    void EndGame()
    {
        if (m_hp <= 0)
        {
            Destroy(gameObject);
            m_resultWindow.SetActive(true);
            m_gm.m_isGameEnd = true;
        }
    }

    /// <summary>移動</summary>
    void Move()
    {
        Vector2 move = m_move.ReadValue<Vector2>();
        if (move != Vector2.zero)
        {
            m_rb.velocity = move * m_moveSpeed;
        }
        else
        {
            m_rb.velocity = Vector2.zero;
        }
    }

    /// <summary>方向転換</summary>
    void Direction()
    {
        Vector2 dir = m_dir.ReadValue<Vector2>();
        if (dir != Vector2.zero)
        {
            transform.up = dir;
        }
    }

    /// <summary>射撃</summary>
    IEnumerator Fire()
    {
        if (!m_bulletPrefab) yield return null; 
        {
            while (true)
            {
                yield return new WaitForSeconds(m_fireDelayTime);
                var go = Instantiate(m_bulletPrefab, m_muzzle.transform.position, Quaternion.identity);
                Vector3 dir = new Vector3(m_muzzle.position.x, m_muzzle.position.y, 0);
                Vector3 shotForward = Vector3.Scale((dir - transform.position), new Vector3(1, 1, 0));
                Vector2 bulletdir = m_dir.ReadValue<Vector2>();
                go.transform.up = bulletdir;
                go.GetComponent<Rigidbody2D>().velocity = shotForward * m_bulletSpeed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HamsterSeed"))
        {
            m_hp--;
            m_souce.PlayOneShot(m_damagedSE);
            EndGame();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PumpkinSeed"))
        {
            m_hp--;
            m_souce.PlayOneShot(m_damagedSE);
            EndGame();
        }
    }
}

