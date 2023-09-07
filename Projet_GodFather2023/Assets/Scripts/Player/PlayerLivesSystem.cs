using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivesSystem : MonoBehaviour
{
    public int Lives;

    [SerializeField] private PlayerMovement m_playerMovement;
    [SerializeField] private LivesUIManager m_UIManager;
    [SerializeField] private GameObject m_gameoverUIManager;
    private void Start()
    {
        m_UIManager.ResetLives(Lives);
    }

    public void Hit()
    {
        if(Lives == 0)
        {
            //Death condition
            m_playerMovement.isAccelerating = false;
            m_gameoverUIManager.SetActive(true);
        }
        else
        {
            //Lose life condition
            Lives--;
            m_UIManager.UpdateLives(Lives);
        }
    }
}
