using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FocusMode : MonoBehaviour
{
    [Header("References")]
    [SerializeField] AudioSource m_audioSource;
    [SerializeField] FocusFiltre m_focusFiltre;
    [SerializeField] PlayerMovement m_playerMovement;
    [SerializeField] MusicManager m_music;

    [Header("Player Input")]
    [SerializeField] InputActionReference
        m_focusButton;

    [Header("Parameters")]
    [SerializeField][Range(0f, 10f)] private float m_maxFocusTime = 4.0f;
    [SerializeField][Range(0f, 1f)] private float m_focusTimeScale = 0.5f;
    [SerializeField][Range(0f, 10f)] private float m_focusRecoverMultiplier = 1f;

    [Header("Sounds")]
    [SerializeField] private AudioClip m_focusOn;
    [SerializeField] private AudioClip m_focusOff;

    [Header("Necessary")]
    private bool m_focusWasPressed = false;
    private float m_currentFocusTime;
    private bool m_isFocusActive = false;
    private bool m_isRecoverActive = false;
    private bool m_isDemo = true;

    public delegate void OnFocusUseDelegate(float value);
    public static event OnFocusUseDelegate OnFocusUse;

    private void OnEnable()
    {
        HomeScreen.OnGameStarted += GameStarted;
    }
    private void OnDisable()
    {
        HomeScreen.OnGameStarted -= GameStarted;
    }


    private void Start()
    {
        GameStarted(false);
    }

    private void FixedUpdate()
    {
        if (!m_isDemo)
        {
            FocusActivation();
            if (m_isRecoverActive && !m_isFocusActive) RecoverFocusTime();
        }

        //Debug.Log("Current Time: " + m_currentFocusTime + " | is Focus active: " + m_isFocusActive + " | Time Scale: " + Time.timeScale + " | isRecover: " + m_isRecoverActive);
    }

    private void FocusActivation()
    {
        if (m_currentFocusTime <= 0)
        {
            PlaySound(m_focusOff);
            StartCoroutine(StopFocus());
        }
        else
        {
            if (m_isFocusActive)
            {
                m_currentFocusTime -= Time.fixedDeltaTime;
                OnFocusUse.Invoke(m_currentFocusTime / m_maxFocusTime);
            }
            FocusAction();
        }
    }

    private void GameStarted(bool _isActive)
    {
        if (_isActive)
        {
            StartCoroutine(m_playerMovement.AnimationStartRunning());
            StartCoroutine(CoolDownDemo(!_isActive));
            m_music.PlayGameMusic();
        }

        m_focusWasPressed = false;
        m_currentFocusTime = m_maxFocusTime;
        m_isFocusActive = false;
        m_isRecoverActive = false;

}

    private void FocusAction()
    {
        bool focusIsPressed;
        if (m_focusButton.action.ReadValue<float>() != 0f) focusIsPressed = true;
        else focusIsPressed = false;

        if (focusIsPressed && !m_focusWasPressed)
        {
            if (Time.timeScale != m_focusTimeScale)
            {
                ChangeTimeScale(m_focusTimeScale, true);
                m_isRecoverActive = false;

                StartCoroutine(m_focusFiltre.StartFiltre());
                PlaySound(m_focusOn);
            }
            else
            {
                PlaySound(m_focusOff);
                StartCoroutine(StopFocus());
            }

            m_focusWasPressed = true;
        }
        else if (!focusIsPressed) m_focusWasPressed = false;
    }

    private void ChangeTimeScale(float _time, bool _isFocusActive)
    {
        Time.timeScale = _time;
        m_isFocusActive = _isFocusActive;
    }

    private void RecoverFocusTime()
    {
        m_currentFocusTime += Time.fixedDeltaTime * m_focusRecoverMultiplier;
        OnFocusUse.Invoke(m_currentFocusTime / m_maxFocusTime);

        if (m_currentFocusTime >= m_maxFocusTime)
        {
            m_isRecoverActive = false;
            m_currentFocusTime = m_maxFocusTime;
        }
    }

    private IEnumerator CoolDownDemo(bool _isDemoActive)
    {
        yield return new WaitForSeconds(0.5f);
        m_isDemo = _isDemoActive;
    }

    private IEnumerator StopFocus()
    {
        ChangeTimeScale(1f, false);
        StartCoroutine(m_focusFiltre.EndFiltre());
        yield return new WaitForSeconds(3f);
        if (!m_isFocusActive) m_isRecoverActive = true;
        yield return null;
    }

    void PlaySound(AudioClip Sound)
    {
        m_audioSource.clip = Sound;
        m_audioSource.Play();
    }
}
