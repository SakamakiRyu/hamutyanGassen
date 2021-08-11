using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class HamsterBullet : MonoBehaviour
{
    /// <summary>弾が破壊するPrefab</summary>
    [SerializeField] GameObject m_brokenPrefab;
    
    float m_time = 0;

    private void Update()
    {
        m_time += Time.deltaTime + 0.01f;
        transform.Rotate(0, 0, m_time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(m_brokenPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
