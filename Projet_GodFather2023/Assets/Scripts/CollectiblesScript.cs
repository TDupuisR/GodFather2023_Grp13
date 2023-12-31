using Shirotetsu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesScript : MonoBehaviour
{
    [SerializeField] GameObject m_visual;
    [SerializeField] AudioSource m_audioSource;
    [SerializeField] AudioClip m_soundCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<ShockWave>().IncreaseGaugePowerUp();
            PlaySound(m_soundCollected);
            m_visual.SetActive(false);
            Destroy(gameObject, m_soundCollected.length);
        }
    }

    void PlaySound(AudioClip Sound)
    {
        m_audioSource.clip = Sound;
        m_audioSource.Play();
    }
}
