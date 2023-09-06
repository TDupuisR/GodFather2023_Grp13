using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreUIScript : MonoBehaviour
{
    [SerializeField]
    PlayerMovement m_playerMovement;

    Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        text.text = m_playerMovement.CalculateScore().ToString();
    }
}
