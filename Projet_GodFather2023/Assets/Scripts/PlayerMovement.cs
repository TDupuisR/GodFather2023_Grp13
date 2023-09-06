using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Input")]
    [SerializeField] InputActionReference
        m_pointerSpeed;

    [Header ("Sensitivity Values")]
    [SerializeField] float m_sensiPlanMovement = 1.0f;
    [SerializeField] float m_smoothSpeed;

    [Header("Plan Clamp")]
    [SerializeField] float m_clampX;
    [SerializeField] float m_clampY;

    [Header("Necessary")]
    Vector3 m_translate;
    float m_currentTranslationX;
    float m_currentTranslationY;


    private void FixedUpdate()
    {
        PlanMovement();
    }

    private void PlanMovement()
    {
        Vector2 movementInput = m_pointerSpeed.action.ReadValue<Vector2>() * m_sensiPlanMovement;

        m_translate = new(Mathf.SmoothDamp(m_translate.x, movementInput.x, ref m_currentTranslationX, m_smoothSpeed),
                          Mathf.SmoothDamp(m_translate.y, movementInput.y, ref m_currentTranslationY, m_smoothSpeed),
                          0f);

        transform.Translate(m_translate);

        transform.position = new(Mathf.Clamp(transform.position.x, -m_clampX, m_clampX),
                                 Mathf.Clamp(transform.position.y, -m_clampY, m_clampY),
                                 transform.position.z);
    }
}
