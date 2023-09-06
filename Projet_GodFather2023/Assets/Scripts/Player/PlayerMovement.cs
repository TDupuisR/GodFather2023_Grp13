using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    [Header("References")]
    [SerializeField] PlayerLivesSystem m_playerLivesSystem;
    [SerializeField] MeshRenderer m_meshRenderer;
    [SerializeField] AudioSource m_audioSource;
    SphereCollider m_collider;

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

    [Space(15)]
    [Header("Knockback")]
    [SerializeField]
    float m_knockbackPower;
    [SerializeField]
    float m_knockbackTime;
    [SerializeField]
    float m_knockbackAlignement;
    [Space(8)]
    [SerializeField]
    float m_RecoveryTime;

    [Header("Acceleration")]
    [SerializeField]
    float m_minSpeed;
    [SerializeField]
    float m_maxSpeed;
    [SerializeField]
    [Range(0f, 1f)]
    float m_speedParaboleProgression;
    [SerializeField]
    float m_speedParaboleSpeedFactor;
    float m_speedParaboleX;
    float m_speed;

    //Score Calculation
    float m_startingZPosition;

    [Header("Sounds")]
    [SerializeField] private AudioClip m_Impact;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //m_meshRenderer = GetComponent<MeshRenderer>();
        m_collider = GetComponent<SphereCollider>();
        m_audioSource.volume = 1f;

        m_speed = m_minSpeed;

        m_startingZPosition = transform.position.z;
    }

    private void FixedUpdate()
    {
        PlanMovement();
        Acceleration();
    }

    private void PlanMovement()
    {
        Vector2 movementInput = m_pointerAcceleration.action.ReadValue<Vector2>() * m_sensiPlanMovement;

        //Calculate and clamp acceleration Vector
        m_translate = new(Mathf.Clamp( Mathf.SmoothDamp(m_translate.x, movementInput.x, ref m_currentTranslationX, m_smoothTime ), -m_clampSpeed, m_clampSpeed),
                          Mathf.Clamp( Mathf.SmoothDamp(m_translate.y, movementInput.y, ref m_currentTranslationY, m_smoothTime ), -m_clampSpeed, m_clampSpeed),
                          0f);

        transform.Translate(m_translate);

        //Keep the player in bound
        transform.position = new(Mathf.Clamp(transform.position.x, -m_clampX, m_clampX),
                                 Mathf.Clamp(transform.position.y, -m_clampY, m_clampY),
                                 transform.position.z);
    }


    private void Acceleration()
    {
        //Add speed
        m_speedParaboleX += Time.deltaTime;
        if (m_speed < m_maxSpeed)
        {
            //Calculate speed
            m_speed = m_speedParaboleProgression * Mathf.Pow(m_speedParaboleX, 2) + m_speedParaboleSpeedFactor * m_speedParaboleX + m_minSpeed;
        }
        else m_speed = m_maxSpeed;

        //Apply Acceleration
        Vector3 Apply_Movement = new Vector3(0, 0, m_speed);
        transform.Translate(Apply_Movement * Time.deltaTime);
    }

    public int CalculateScore()
    {
        int score = (int)(transform.position.z - m_startingZPosition);
        return score;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            //Reset speed
            m_speed = m_minSpeed;
            m_speedParaboleX = 0;

            //Knockback
            rb.AddForce(new Vector3(0f, 0f, -m_knockbackPower));

            StartCoroutine(KnockBackEffect(m_knockbackTime));
            StartCoroutine(RecoveryTime(m_RecoveryTime));

            //Send Hit & Play sound
            m_playerLivesSystem.Hit();
            PlaySound(m_Impact);

            //Reposition player
            //Guess offset with object
            Vector2 offsetWithObject = new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y);

            if (offsetWithObject.x > 0) rb.AddForce(m_knockbackAlignement, 0f, 0f); //Perso est à droite
            else if (offsetWithObject.x < 0) rb.AddForce(-m_knockbackAlignement, 0f, 0f); //Perso est à gauche

            if (offsetWithObject.y > 0) rb.AddForce(0f, m_knockbackAlignement, 0f);//Perso est en haut
            else if (offsetWithObject.y < 0) rb.AddForce(0, -m_knockbackAlignement, 0f);//Perso est en bas
        }
    }

    IEnumerator KnockBackEffect(float _time)
    {
        yield return new WaitForSeconds(_time); //Wait before reset knockback
        rb.velocity = Vector3.zero;

        //print("RESET");
    }

    IEnumerator RecoveryTime(float _time)
    {
        m_collider.enabled = false;
        for (int i = 0; i < _time * 8; i++)
        {
            m_meshRenderer.enabled = false;
            yield return new WaitForSeconds(_time / 8);
            m_meshRenderer.enabled = true;
            yield return new WaitForSeconds(_time / 8);
        }
        m_collider.enabled = true;
    }

    void PlaySound(AudioClip Sound)
    {
        m_audioSource.clip = Sound;
        m_audioSource.Play();
    }
}
