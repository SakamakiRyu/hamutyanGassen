using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    /// <summary>カウントダウンオブジェクト</summary>    
    [SerializeField] GameObject m_countDown;
    /// <summary>ゲームスタート時の座標</summary>
    [SerializeField] Transform[] m_staticObjects;
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
    /// <summary>パンプキンの生成間隔</summary>
    [SerializeField] float m_createPumpkinDeleyTime = 0;
    /// <summary>カウントダウン</summary>
    [SerializeField] AudioClip m_countDownSE;
    /// <summary>スタート</summary>
    [SerializeField] AudioClip m_startSE;
    /// <summary>リザルト</summary>
    [SerializeField] AudioClip m_result;

    AudioSource m_souce;
    Hamster[] m_hamsters;
    public bool m_isGameEnd { get; set; } = false;
    bool m_isStarted = false;
    bool m_isCreatePumpkin = false;
    bool m_isChengeBGM = false;
    bool m_isPlaySE = false;
    float m_time;

    private void Start()
    {
        m_time = m_result.length;
        m_souce = GetComponent<AudioSource>();
        ObjectSetter();
        Reflash();
    }

    private void Update()
    {
        if (!m_isGameEnd)
        {
            if (m_isStarted && !m_isCreatePumpkin)
            {
                StartCoroutine(CreatePumpkin());
                m_isCreatePumpkin = true;
            }
        }
        else
        {
            var go = GameObject.FindGameObjectsWithTag("Pumpkin");
            foreach (var item in go)
            {
                item.SetActive(false);
            }
            if (!m_isChengeBGM)
            {
                if (!m_isPlaySE)
                {
                    m_souce.PlayOneShot(m_result);
                    m_isPlaySE = true;
                }
                m_time -= Time.deltaTime;
                if (m_time <= 0)
                {
                    m_isChengeBGM = true;
                }
            }
        }
    }

    /// <summary>かぼちゃの自動生成</summary>
    IEnumerator CreatePumpkin()
    {
        int rnd;
        int createCount = 0;
        bool isSpeedUp = false;
        while (true)
        {
            if (m_isGameEnd)
            {
                break;
            }
            yield return new WaitForSeconds(m_createPumpkinDeleyTime);
            rnd = UnityEngine.Random.Range(0, m_startPositions.Length - 1);
            Instantiate(m_bomb, m_startPositions[rnd].position, Quaternion.identity);
            createCount++;
            if (createCount > 10 && !isSpeedUp)             // 10個生成したら生成時間の間隔を短くする
            {
                m_createPumpkinDeleyTime -= 1;              // 生成の間隔を縮める
                isSpeedUp = true;
            }
        }
    }

    /// <summary>オブジェクトの</summary>
    void ObjectSetter()
    {
        List<int> numbers = new List<int>();
        for (int i = 0; i < m_startPositions.Length - 1; i++)
        {
            numbers.Add(i);
        }
        var numbersArray = numbers.OrderBy(x => Guid.NewGuid()).ToArray();

        for (int i = 0; i < m_staticObjects.Length; i++)
        {
            m_staticObjects[i].position = m_startPositions[numbersArray[i]].position;
        }
    }

    /// <summary>ゲームスタート処理</summary>
    void Reflash()
    {
        m_hamsters = FindObjectsOfType<Hamster>();
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
        for (int i = m_startCount - 1; i >= -1; i--)
        {
            if (i >= 0)
            {
                m_souce.PlayOneShot(m_countDownSE);
                yield return new WaitForSeconds(1f);
                m_displayNumber.sprite = m_nubers[i];
            }
            else
            {
                m_souce.PlayOneShot(m_startSE);
            }

        }
        foreach (var item in m_hamsters)
        {
            item.IsStarted = true;
        }
        m_isStarted = true;
        Destroy(m_countDown);
    }
}
