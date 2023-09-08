using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivesSystem : MonoBehaviour
{
    public int Lives;

    [SerializeField] private LivesUIManager m_UIManager;
    [SerializeField] private GameObject m_gameoverUIManager;
    [SerializeField] private PlayerMovement m_playerMovement;
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
}
