using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardLine : MonoBehaviour
{
    [SerializeField] Text m_scoreText;
    
    public void SetScoreValue(int value, int order)
    {
        int TextOrder = order + 1;
        m_scoreText.text = TextOrder.ToString() + " - " + value.ToString();
        transform.localPosition = new(0f, 20f + (-50f * order), 0f);
    }
}
