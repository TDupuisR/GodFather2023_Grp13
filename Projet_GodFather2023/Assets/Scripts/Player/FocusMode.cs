using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FocusMode : MonoBehaviour
{
    [Header("Player Input")]
    [SerializeField] InputActionReference
        m_focusButton;

    [Header("Parameters")]
    [SerializeField][Range(0f, 10f)] private float m_maxFocusTime = 4.0f;
    [SerializeField][Range(0f, 1f)] private float m_focusTimeScale = 0.5f;
    [SerializeField][Range(0f, 10f)] private float m_focusRecoverMultiplier = 1f;

    [Header("Necessary")]
    private bool m_focusWasPressed = false;
    private float m_currentFocusTime;
    private bool m_isFocusActive = false;
    private bool m_isRecoverActive = false;

    public delegate void OnFocusUseDelegate(float value);
    public static event OnFocusUseDelegate OnFocusUse;

    private void Start()
    {
        m_currentFocusTime = m_maxFocusTime;
        OnFocusUse.Invoke(m_currentFocusTime);
    }

    private void FixedUpdate()
    {
        FocusActivation();
        if (m_isRecoverActive && !m_isFocusActive) RecoverFocusTime();

        Debug.Log("Current Time: " + m_currentFocusTime + " | is Focus active: " + m_isFocusActive + " | Time Scale: " + Time.timeScale + " | isRecover: " + m_isRecoverActive);
    }

    private void FocusActivation()
    {
        if (m_currentFocusTime <= 0)
        {
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
            }
            else
            {
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

    private IEnumerator StopFocus()
    {
        ChangeTimeScale(1f, false);
        yield return new WaitForSeconds(3f);
        if (!m_isFocusActive) m_isRecoverActive = true;
        yield return null;
    }
}
