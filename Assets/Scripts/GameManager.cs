using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>カウントダウンオブジェクト</summary>    
    [SerializeField] GameObject m_countDown;
    /// <summary>ゲームスタート時の</summary>
    [SerializeField] GameObject[] m_startObjects;
    /// <summary>自動生成する爆弾</summary>
    [SerializeField] GameObject m_bomb;
    /// <summary>数字を表示するImage</summary>
    [SerializeField] Image m_displayNumber;
    /// <summary>カウントダウンに使う数字のSprite</summary>
    [SerializeField] Sprite[] m_nubers;
    /// <summary></summary>
    [SerializeField] Transform[] m_startPositions;
    /// <summary>カウントの初期値</summary>
    [SerializeField] int m_startCount;

    bool m_isStarted = false;

    private void Start()
    {
        Reflash();
    }

    /// <summary>カウントダウン</summary>
    IEnumerator CountDown()
    {
        for (int i = m_startCount - 1; i > -1; i--)
        {
            yield return new WaitForSeconds(1f);
            m_displayNumber.sprite = m_nubers[i];
        }
        m_isStarted = true;
        Destroy(m_countDown);
    }

    /// <summary>ゲームスタート処理</summary>
    void Reflash()
    {
        if (m_startCount > m_nubers.Length)
        {
            m_startCount = m_nubers.Length - 1;
        }
        m_displayNumber.sprite = m_nubers[m_startCount];
        StartCoroutine(CountDown());
    }
}
