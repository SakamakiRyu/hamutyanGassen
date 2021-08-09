using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
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
        StartCoroutine(Fire());
    }

    private void Update()
    {
        Move();
        Direction();
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
    IEnumerator Fire()   // 後に修正
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
}

