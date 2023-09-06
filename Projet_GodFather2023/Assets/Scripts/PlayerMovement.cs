using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Input")]
    [SerializeField] InputActionReference
        m_pointerAcceleration;

    [Header ("Sensitivity Values")]
    [SerializeField][Range(0.0f, 0.5f)] float m_sensiPlanMovement = 1.0f;
    [SerializeField][Range(0.01f, 0.5f)] float m_smoothTime;


    [Header("Clamp")]
    [SerializeField][Range(0.0f , 10.0f)] float m_clampX;
    [SerializeField][Range(0.0f, 10.0f)] float m_clampY;
    [SerializeField][Range(0.0f, 2.0f)] float m_clampSpeed;

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
        Vector2 movementInput = m_pointerAcceleration.action.ReadValue<Vector2>() * m_sensiPlanMovement;

        m_translate = new(Mathf.Clamp( Mathf.SmoothDamp(m_translate.x, movementInput.x, ref m_currentTranslationX, m_smoothTime ), -m_clampSpeed, m_clampSpeed),
                          Mathf.Clamp( Mathf.SmoothDamp(m_translate.y, movementInput.y, ref m_currentTranslationY, m_smoothTime ), -m_clampSpeed, m_clampSpeed),
                          0f);

        transform.Translate(m_translate);

        transform.position = new(Mathf.Clamp(transform.position.x, -m_clampX, m_clampX),
                                 Mathf.Clamp(transform.position.y, -m_clampY, m_clampY),
                                 transform.position.z);
    }
}
