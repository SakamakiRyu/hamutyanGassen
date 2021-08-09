using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBullet : MonoBehaviour
{
    /// <summary>破棄する時間</summary>
    [SerializeField] float m_destroyTime;

    void Start()
    {
        StartCoroutine(Destroy());
    }

    /// <summary>削除</summary>
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(m_destroyTime);
        Destroy(gameObject);
    }
}
