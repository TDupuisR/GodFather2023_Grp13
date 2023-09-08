using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivesSystem : MonoBehaviour
{
    public int Lives;

    [SerializeField] private LivesUIManager m_UIManager;
    [SerializeField] private GameObject m_gameoverUIManager;
    [SerializeField] private PlayerMovement m_playerMovement;
    [SerializeField] private FocusMode m_focusScript;

    public delegate void OnGameOverDelegate(bool value);
    public static OnGameOverDelegate OnGameOver;

    private void Start()
    {
        m_UIManager.ResetLives(Lives);
    }

    public void Hit()
    {
        if(Lives == 0)
        {
            //Death condition
            m_focusScript.enabled = false;
            Time.timeScale = 1.0f;
            StartCoroutine(m_playerMovement.AnimationEndRunning());
            m_gameoverUIManager.SetActive(true);
            OnGameOver.Invoke(false);
        }
        else
        {
            //Lose life condition
            Lives--;
            m_UIManager.UpdateLives(Lives);
        }
    }

    
    /*private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 50, 150, 500));
        GUILayout.BeginVertical();

        if (GUILayout.Button("Life"))
        {
            Hit();
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }*/
}
