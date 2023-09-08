using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    [SerializeField] AudioSource m_audioSource;
    [SerializeField] AudioClip m_menuMusic;
    [SerializeField] AudioClip m_gameMusic;

    private void Start()
    {
        m_audioSource.clip = m_menuMusic;
        m_audioSource.Play();
    }

    public void PlayGameMusic()
    {
        m_audioSource.clip = m_gameMusic;
        m_audioSource.Play();
    }
}
