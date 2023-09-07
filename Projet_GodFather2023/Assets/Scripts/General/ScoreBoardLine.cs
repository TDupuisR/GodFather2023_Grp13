using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardLine : MonoBehaviour
{
    [SerializeField] Text m_scoreText;

    public void SetScoreValue(int value, int order)
    {
        m_scoreText.text = value.ToString();
        transform.localPosition = new(0f, 20f + (-50f * order), 0f);
    }
}
