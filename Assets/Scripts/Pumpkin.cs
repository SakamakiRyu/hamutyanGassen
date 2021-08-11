using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    /// <summary>弾</summary>
    [SerializeField] GameObject m_bullet;
    /// <summary>爆発するプレハブ</summary>
    [SerializeField] GameObject m_explosion;
    /// <summary>生成する弾の数</summary>
    [SerializeField] int m_createBulletAmount = 0;
    /// <summary></summary>
    [SerializeField] float m_bulletSpeed = 0;
    /// <summary>弾の生成間隔</summary>
    [SerializeField] float m_createDelayTime = 0;
    /// <summary>爆弾の体力</summary>
    [SerializeField] int m_hp = 0;
   
    CircleCollider2D m_collider;

    private void Start()
    {
        m_collider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HamsterSeed") || collision.CompareTag("PumpkinSeed"))
        {
            m_hp--;
            if (m_hp <= 0)
            {
                StartCoroutine(Explosion());
            }
        }
    }

    IEnumerator Explosion()
    {
        m_collider.enabled = false;
        for (int i = 0; i < m_createBulletAmount; i++)
        {
            var go = Instantiate(m_bullet, transform.position, Quaternion.identity);
            go.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized * m_bulletSpeed;
            yield return new WaitForSeconds(m_createDelayTime);
        }
        Instantiate(m_explosion,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
