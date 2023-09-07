using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    [SerializeField] private ScoreSaver m_scoreSaverScript;
    [SerializeField] private GameObject m_prefabScoreLine;
    [SerializeField] private Transform m_HighScoreLayoutTransform;

    [SerializeField] public Slider m_holdCircle;

    [SerializeField] InputActionReference
    m_focusButton,
    m_shockwaveButton;

    private float m_circleProgression = 0;

    public delegate void OnGameStartDelegate(bool isPlayStarted);
    public static OnGameStartDelegate OnGameStarted;

    private void Awake()
    {
        LeaderBoardSetup();
        //OnGameStarted.Invoke(false);
    }

    private void Update()
    {
        HoldToStart();
    }

    private void LeaderBoardSetup()
    {
        int[] scoresBoard = m_scoreSaverScript.GiveStoredScore();
        int numberOfScores = 9;

        if (PlayerPrefs.GetInt("index") < 9) numberOfScores = PlayerPrefs.GetInt("index");

        for (int i = 0; i <= numberOfScores; i++)
        {
            Debug.Log("Score Board Line N°" + i);
            GameObject prefab = Instantiate(m_prefabScoreLine, transform);
            prefab.transform.SetParent(m_HighScoreLayoutTransform);
            //prefab.GetComponent<ScoreBoardLine>().SetScoreValue(scoresBoard[i], i);
        }
    }

    private void HoldToStart()
    {
        if (m_circleProgression < 1f)
        {
            if (m_focusButton.action.ReadValue<float>() != 0f && m_shockwaveButton.action.ReadValue<float>() != 0f)
            {
                m_circleProgression += (Time.deltaTime / Time.timeScale);
            }
            else if (m_circleProgression > 0f)
            {
                m_circleProgression -= (Time.deltaTime / Time.timeScale);
            }

            m_holdCircle.value = m_circleProgression;
        }
        else
        {
            OnGameStarted.Invoke(true);
            gameObject.SetActive(false);
        }
    }
}
