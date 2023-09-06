using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivesSystem : MonoBehaviour
{
    public int Lives;

    [SerializeField] private LivesUIManager m_UIManager;

    private void Start()
    {
        m_UIManager.ResetLives(Lives);
    }

    public void Hit()
    {
        if(Lives == 0)
        {
            //Death condition
        }
        else
        {
            //Lose life condition
            Lives--;
            m_UIManager.UpdateLives(Lives);
        }
    }
}
