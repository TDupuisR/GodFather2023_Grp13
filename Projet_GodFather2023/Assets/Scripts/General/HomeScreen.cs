using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    [SerializeField] private ScoreSaver m_scoreSaverScript;
    [SerializeField] private ScoreBoardLine m_prefabScoreLine;
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
    }

    private void Update()
    {
        HoldToStart();
    }

    private void LeaderBoardSetup()
    {
        (int[] scoresBoard, string[] nameBoard) = m_scoreSaverScript.GiveStoredScore();
        int numberOfScores = 9;

        if (PlayerPrefs.HasKey("index"))
        {
            if (PlayerPrefs.GetInt("index") < 9) numberOfScores = PlayerPrefs.GetInt("index");

            for (int i = 0; i <= numberOfScores; i++)
            {
                ScoreBoardLine prefab = Instantiate(m_prefabScoreLine, m_HighScoreLayoutTransform);
                prefab.SetScoreValue(scoresBoard[i], nameBoard[i], i);
            }
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
