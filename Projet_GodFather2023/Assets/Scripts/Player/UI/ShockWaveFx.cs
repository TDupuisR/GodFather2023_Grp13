using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShockWaveFx : MonoBehaviour
{
    [SerializeField] Image m_filtreImage;

    [SerializeField] Color m_filtreColor;
    [SerializeField][Range(1f, 10f)] float m_time;

    public IEnumerator StartFX()
    {
        m_filtreImage.color = new Color(m_filtreColor.r, m_filtreColor.g, m_filtreColor.b, 1f);

        float timeElapsed = 1f;
        while (timeElapsed > 0f)
        {
            m_filtreImage.color = new Color(m_filtreColor.r, m_filtreColor.g, m_filtreColor.b, timeElapsed);

            yield return null;
            timeElapsed -= Time.deltaTime * m_time;
        }

        m_filtreImage.color = new Color(m_filtreColor.r, m_filtreColor.g, m_filtreColor.b, 0f);
    }
}
