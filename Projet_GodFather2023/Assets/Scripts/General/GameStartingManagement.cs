using UnityEngine;

public class GameStartingManagement : MonoBehaviour
{
    [SerializeField] GameObject m_PlayerUI;

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
        m_PlayerUI.SetActive(_isActive);
    }

}
