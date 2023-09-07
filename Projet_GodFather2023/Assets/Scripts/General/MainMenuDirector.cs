using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MainMenuDirector : MonoBehaviour
{
    [SerializeField] PlayableDirector m_director;
    [SerializeField] List<GameObject> m_placeholderActor = new List<GameObject>();

    private GameObject[] m_highscoretext;
    void Start()
    {
        m_highscoretext = GameObject.FindGameObjectsWithTag("HScoreText");

    }
}
