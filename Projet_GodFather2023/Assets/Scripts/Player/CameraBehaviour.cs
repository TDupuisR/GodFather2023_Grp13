using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [Header ("Parameters")]
    [SerializeField] Transform m_playerPosition;
    [SerializeField] Vector3 m_offset = new(0.0f, 0.0f, -6.0f);
    [SerializeField] float m_timeOffset;


    [Header("Necessary")]
    Vector3 m_currentSmoothSpeed;

    private void Awake()
    {
        transform.position = m_playerPosition.transform.position + m_offset;
    }

    private void FixedUpdate()
    {
        CameraFollow();
    }

    void CameraFollow()
    {
        Vector3 zPlayerPosition = new(0f, 0f, m_playerPosition.transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, zPlayerPosition + m_offset, ref m_currentSmoothSpeed, m_timeOffset);
    }
}
