using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D),typeof(Rigidbody2D))]
public class PumpkinBullet : MonoBehaviour
{
    [SerializeField] GameObject m_brokenPrefab;

    /// <summary>壁に何回接触したら壊れるか(回数)</summary>
    [SerializeField] int m_wallHitCountLimit = 2;

    int m_wallHitCount = 0;

    float m_time = 0;

    private void Update()
    {
        m_time += Time.deltaTime + 0.01f;
        transform.Rotate(0, 0, m_time);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            m_wallHitCount++;
        }
        if (collision.gameObject.CompareTag("Hamster")|| m_wallHitCount == m_wallHitCountLimit)
        {
            Destroyer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pumpkin"))
        {
            Destroyer();
        }
    }

    /// <summary>破棄処理 + α</summary>
    void Destroyer()
    {
        Instantiate(m_brokenPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
