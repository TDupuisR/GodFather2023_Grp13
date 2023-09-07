using UnityEngine;

public class GameStartingManagement : MonoBehaviour
{
    [SerializeField] GameObject m_playerUI;
    [SerializeField] PlayerMovement m_playerMove;

    private void OnEnable()
    {
        HomeScreen.OnGameStarted += ActivateAll;
    }
    private void OnDisable()
    {
        HomeScreen.OnGameStarted -= ActivateAll;
    }

    private void ActivateAll(bool _isActive)
    {
        m_playerUI.SetActive(_isActive);
        m_playerMove.isAccelerating = true;
    }

}
