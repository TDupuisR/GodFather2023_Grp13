using UnityEngine;


public class ScoreSaver : MonoBehaviour
{
    private void OnEnable()
    {
        GameOverUIManager.OnGameOverScreen += SaveStoredScore;
    }

    private void OnDisable()
    {
        GameOverUIManager.OnGameOverScreen -= SaveStoredScore;
    }


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

    public int[] GiveStoredScore()
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
                        for (int k = index; k > j; k--)
                        {
                            tab[k] = tab[k-1];
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
    }


    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 50, 150, 500));
        GUILayout.BeginVertical();

        /*if (GUILayout.Button("Save"))
        {
            SaveStoredScore(m_scoreValue);
        }
        if (GUILayout.Button("Get"))
        {
            GiveStoredScore();
        }*/
        if (GUILayout.Button("Reset"))
        {
            ResetStoredScore();
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
