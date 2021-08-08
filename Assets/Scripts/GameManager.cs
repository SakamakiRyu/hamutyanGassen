using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>カウントダウンに使う数字のSprite</summary>
    [SerializeField] Sprite[] m_nubers;
    /// <summary>数字を表示するImage</summary>
    [SerializeField] Image m_displayNumber;
    /// <summary>カウントダウンオブジェクト</summary>    
    [SerializeField] GameObject m_countDown;
    /// <summary>カウントの初期値</summary>
    [SerializeField] int m_startCount;
    /// <summary>プレイヤー</summary>
    [SerializeField] GameObject[] m_players;

    bool m_isStarted = false;

    private void Start()
    {
        if (m_startCount > m_nubers.Length)
        {
            m_startCount = m_nubers.Length - 1;
        }
        m_displayNumber.sprite = m_nubers[m_startCount];
        StartCoroutine(CountDown());
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
}
