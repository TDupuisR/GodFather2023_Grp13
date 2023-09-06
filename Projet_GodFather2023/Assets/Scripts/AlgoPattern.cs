using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgoPattern : MonoBehaviour
{
    [SerializeField]
    List<GameObject> m_PatternList = new List<GameObject>();

    [Space(5)]
    [Header("Parameters")]
    [SerializeField]
    int m_numberPatterns;
    [SerializeField]
    float m_distanceBtwPatterns;
    
    int m_lastPattern;
    int m_spawnIndex;

    private void Start()
    {
        ResetLevel();
    }

    public void ResetLevel()
    {
        for (int i = 0; i < m_numberPatterns; i++)
        {
            while (m_spawnIndex == m_lastPattern)
            {
                int m_spawnIndex = Random.Range(0, m_PatternList.Count);
            }

            GameObject newPattern = Instantiate(m_PatternList[m_spawnIndex], transform);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + m_distanceBtwPatterns);
            m_spawnIndex = m_lastPattern;
        }
    }
}
