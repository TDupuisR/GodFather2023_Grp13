using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameSelector : MonoBehaviour
{
    [Header("Text Frames")]
    [SerializeField] private Text m_letterOne;
    [SerializeField] private Text m_letterTwo;
    [SerializeField] private Text m_letterThree;

    [SerializeField] private Text m_validText;

    [Header("Parameters")]
    [SerializeField][Range(0f, 5f)] private float m_alphabetSensi;

    [Header("Input")]
    [SerializeField] private InputActionReference
    m_pointerAcceleration,
    m_leftButton,
    m_rightButton;

    [Header("Necessary")]
    private float m_cumulPointer = 0f;
    private Text[] m_letterIndex;
    private int[] m_indexAlphabet = new int[3];
    private int m_currentLetterIndex = 0;
    private string[] m_alphabetList =
    {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
        "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "_"
    };
    private int m_currentAlphabetIndex = 0;
    private bool m_wasPressed = false;
    private bool m_selectionActive = true;
    public int finalScore;


    public delegate void OnRestartSceneDelegate(int score, string name);
    public static OnRestartSceneDelegate OnRestartScene;

    private void OnEnable()
    {
        m_letterIndex = new Text[3] {m_letterOne, m_letterTwo, m_letterThree};
        StartCoroutine(CountdownUntilRestart(60f));
    }

    private void FixedUpdate()
    {
        if (m_selectionActive)
        {
            PositionSelector();
            LetterSelector();
            ValidName();
        }
    }

    private void PositionSelector()
    {
        if (m_leftButton.action.ReadValue<float>() > 0f && m_currentLetterIndex > 0 && !m_wasPressed)
        {
            m_indexAlphabet[m_currentLetterIndex] = m_currentAlphabetIndex;
            m_currentLetterIndex--;
            m_currentAlphabetIndex = m_indexAlphabet[m_currentLetterIndex];

            m_wasPressed = true;
        }
        else if (m_rightButton.action.ReadValue<float>() > 0f && m_currentLetterIndex < 2 && !m_wasPressed)
        {
            m_indexAlphabet[m_currentLetterIndex] = m_currentAlphabetIndex;
            m_currentLetterIndex++;
            m_currentAlphabetIndex = m_indexAlphabet[m_currentLetterIndex];

            m_wasPressed = true;
        }
        else if (m_leftButton.action.ReadValue<float>() == 0f && m_rightButton.action.ReadValue<float>() == 0f && m_wasPressed)
        {
            m_wasPressed = false;
        }
    }

    private void LetterSelector()
    {
        Vector2 pointer = m_pointerAcceleration.action.ReadValue<Vector2>();
        m_cumulPointer += pointer.y;

        if (m_cumulPointer >= m_alphabetSensi)
        {
            m_currentAlphabetIndex++;
            m_cumulPointer = 0;
        }
        else if (m_cumulPointer <= -m_alphabetSensi)
        {
            m_currentAlphabetIndex--;
            m_cumulPointer = 0;
        }

        if (m_currentAlphabetIndex < 0) m_currentAlphabetIndex = m_alphabetList.Length - 1;
        else if (m_currentAlphabetIndex >= m_alphabetList.Length) m_currentAlphabetIndex = 0;

        m_letterIndex[m_currentLetterIndex].text = m_alphabetList[m_currentAlphabetIndex];
    }

    private void ValidName()
    {
        if (m_rightButton.action.ReadValue<float>() > 0f && m_leftButton.action.ReadValue<float>() > 0f)
        {
            m_selectionActive = false;
            m_validText.gameObject.SetActive(false);

            string name = m_letterIndex[0].text + m_letterIndex[1].text + m_letterIndex[2].text;
            OnRestartScene.Invoke(finalScore, name);

            StopAllCoroutines();
            StartCoroutine(CountdownUntilRestart(5f));
        }
    }

    IEnumerator CountdownUntilRestart(float _countdown)
    {
        yield return new WaitForSeconds(_countdown);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
