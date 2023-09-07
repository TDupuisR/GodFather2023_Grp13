using UnityEngine;
using UnityEngine.InputSystem;

public class HomeScreen : MonoBehaviour
{
    [SerializeField] private ScoreSaver m_scoreSaverScript;
    [SerializeField] private GameObject m_prefabScoreLine;

    [SerializeField] InputActionReference
    m_focusActionButton,
    m_shockwaveActionButton;

    public delegate void OnGameStartDelegate(bool isPlayStarted);
    public static OnGameStartDelegate OnGameStarted;

    private void Start()
    {
        LeaderBoardSetup();
        OnGameStarted.Invoke(false);
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
            prefab.GetComponent<ScoreBoardLine>().SetScoreValue(scoresBoard[i], i);
        }
    }



}
