using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerMovement m_playermovement;
    [SerializeField] Image m_backgroundImage;
    [SerializeField] Text m_finalScoreText;
    [Header("Parameters")]
    [SerializeField] float m_backgroundFadeIn;
    [SerializeField] float m_countdownRestart;

    public delegate void OnGameOverScreenDelegate(int score);
    public static OnGameOverScreenDelegate OnGameOverScreen;

    private void OnEnable()
    {
        int finalScore = m_playermovement.CalculateScore();
        m_finalScoreText.text = "Final score : " + finalScore.ToString();
        OnGameOverScreen.Invoke(finalScore);
        StartCoroutine(BackgroundFadeIn());
        StartCoroutine(CountdownUntilRestart());
    }

    IEnumerator BackgroundFadeIn()
    {
        m_backgroundImage.color = new Color(0f, 0f, 0f, 0f);

        float timeElapsed = 0;
        while (timeElapsed < 1f)
        {
            print(timeElapsed);
            m_backgroundImage.color = new Color(0f, 0f, 0f, timeElapsed);

            yield return null;
            timeElapsed += Time.deltaTime / m_backgroundFadeIn;
        }
    }
    IEnumerator CountdownUntilRestart()
    {
        yield return new WaitForSeconds(m_countdownRestart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
