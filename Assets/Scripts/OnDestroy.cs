using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroy : MonoBehaviour
{
    /// <summary>破棄する時間</summary>
    [SerializeField] float m_destroyTime;

    [SerializeField] GameObject m_createPrefab = null;

    void Start()
    {
        StartCoroutine(Destroy());
    }

    /// <summary>削除</summary>
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(m_destroyTime);
        if (m_createPrefab)
        {
            Instantiate(m_createPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
