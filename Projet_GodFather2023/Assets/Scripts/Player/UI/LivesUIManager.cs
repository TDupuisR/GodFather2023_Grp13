using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUIManager : MonoBehaviour
{
    [SerializeField]
    private List<Image> LivesImages = new List<Image>();

    private void Awake()
    {
        foreach(Image image in LivesImages)
        {
            image.enabled = true;
        }
    }

    private void OnEnable()
    {
        //PlayerMovement += UpdateLives;
    }

    private void OnDisable()
    {
        //PlayerMovement -= UpdateLives;
    }

    public void ResetLives()
    {
        foreach (Image image in LivesImages)
        {
            image.enabled = true;
        }
    }

    void UpdateLives(int lives)
    {
        for (int i= 0; i < LivesImages.Count; i++)
        {
            LivesImages[i].enabled = false;
        }
    }
}
