using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using OpenCover.Framework.Model;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreSaver : MonoBehaviour
{
    [SerializeField] int m_scoreValue;

    void SaveStoredScore(int score)
    {
        if (!PlayerPrefs.HasKey("index"))
        {
            PlayerPrefs.SetInt("index", 0);
            PlayerPrefs.SetInt("score0", score);

            Debug.Log("index 0+ " + score);
        }
        else
        {
            int index = PlayerPrefs.GetInt("index");
            string scoreTag = "score" + (index+1).ToString();

            PlayerPrefs.SetInt("index", index + 1);
            PlayerPrefs.SetInt(scoreTag, score);

            Debug.Log("index 1+ " + scoreTag + " | " + score);
        }
    }

    private int[] GetStoredScore()
    {
        int[] tab = new int[0];

        if (PlayerPrefs.HasKey("index"))
        {
            int index = PlayerPrefs.GetInt("index");
            tab = new int[index+1];

            for (int i = 0; i <= index; i++)
            {
                string askIndex = "score"+ i.ToString();
                int score = PlayerPrefs.GetInt(askIndex);

                for (int j = 0; j <= i; j++)
                {
                    if (score > tab[j])
                    {
                        for (int k = index-1; k > j; k--)
                        {
                            tab[k+1] = tab[k];
                        }

                        tab[j] = score;
                        j = i + 1;
                    }
                }
            }
        }

        return tab;
    }

    private void ResetStoredScore()
    {
        PlayerPrefs.DeleteAll();

        Debug.Log("index reset");
    }


    /*private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 50, 150, 500));
        GUILayout.BeginVertical();

        if (GUILayout.Button("Save"))
        {
            SaveStoredScore(m_scoreValue);
        }
        if (GUILayout.Button("Get"))
        {
            GetStoredScore();
        }
        if (GUILayout.Button("Reset"))
        {
            ResetStoredScore();
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }*/
}