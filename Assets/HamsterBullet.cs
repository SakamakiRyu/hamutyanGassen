using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class HamsterBullet : MonoBehaviour
{
    /// <summary>弾が破壊するPrefab</summary>
    [SerializeField] GameObject m_brokenPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(m_brokenPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
