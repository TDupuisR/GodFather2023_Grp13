using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUIManager : MonoBehaviour
{
    [SerializeField] private List<Image> LivesImages = new List<Image>();
    [SerializeField] private Sprite m_lifeSprite;
    [SerializeField] private Sprite m_deadSprite;
    private int m_maxLives;

    private void Awake()
    {
        foreach(Image image in LivesImages)
        {
            image.sprite = m_lifeSprite;
        }
    }

    /*
    private void OnEnable()
    {
        //PlayerMovement += UpdateLives;
    }

    private void OnDisable()
    {
        //PlayerMovement -= UpdateLives;
    }
    */

    public void ResetLives(int _maxlives)
    {
        m_maxLives = _maxlives;

        foreach (Image image in LivesImages)
        {
            image.enabled = true;
        }
    }

    public void UpdateLives(int lives)
    {
        for (int i= 0; i < m_maxLives - lives; i++)
        {
            LivesImages[i].sprite = m_deadSprite;
        }
    }
}
