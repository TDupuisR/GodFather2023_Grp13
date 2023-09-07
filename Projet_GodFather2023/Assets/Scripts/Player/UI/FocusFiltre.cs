using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusFiltre : MonoBehaviour
{
    [SerializeField] Image m_filtreImage;

    [SerializeField] Color m_filtreColor;
    [SerializeField] [Range(0f, 1f)] float m_percent;
    [SerializeField] [Range(1f , 10f)]float m_time;

    public IEnumerator StartFiltre()
    {
        m_filtreImage.color = new Color(m_filtreColor.r, m_filtreColor.g, m_filtreColor.b, 0f);

        float timeElapsed = 0;
        while (timeElapsed < 1f)
        {
            print(timeElapsed);
            m_filtreImage.color = new Color(m_filtreColor.r, m_filtreColor.g, m_filtreColor.b, m_percent*timeElapsed);

            yield return null;
            timeElapsed += Time.deltaTime * m_time;
        }
    }
    public IEnumerator EndFiltre()
    {
        m_filtreImage.color = new Color(m_filtreColor.r, m_filtreColor.g, m_filtreColor.b, m_percent);

        float timeElapsed = 1;
        while (timeElapsed > 0f)
        {
            print(timeElapsed);
            m_filtreImage.color = new Color(m_filtreColor.r, m_filtreColor.g, m_filtreColor.b, timeElapsed);

            yield return null;
            timeElapsed -= Time.deltaTime * m_time;
        }
    }
}
