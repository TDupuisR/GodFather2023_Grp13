using UnityEngine;


public class ScoreSaver : MonoBehaviour
{
    private void OnEnable()
    {
        NameSelector.OnRestartScene += SaveStoredScore;
    }

    private void OnDisable()
    {
        NameSelector.OnRestartScene -= SaveStoredScore;
    }


    void SaveStoredScore(int score, string name)
    {
        if (!PlayerPrefs.HasKey("index"))
        {
            PlayerPrefs.SetInt("index", 0);

            PlayerPrefs.SetInt("score0", score);
            PlayerPrefs.SetString("name0", name);
            

            //Debug.Log("index 0+ " + score + " | " + name);
        }
        else
        {
            int index = PlayerPrefs.GetInt("index");
            string scoreTag = "score" + (index+1).ToString();
            string nameTag = "name" + (index+1).ToString();

            PlayerPrefs.SetInt("index", index + 1);

            PlayerPrefs.SetInt(scoreTag, score);
            PlayerPrefs.SetString(nameTag, name);

            Debug.Log("index 1+ " + scoreTag + " | " + score + " | " + name);
        }
    }

    public (int[], string[]) GiveStoredScore()
    {
        int[] tabScore = new int[0];
        string[] tabName = new string[0];

        if (PlayerPrefs.HasKey("index"))
        {
            int index = PlayerPrefs.GetInt("index");
            tabScore = new int[index+1];
            tabName = new string[index+1];

            for (int i = 0; i <= index; i++)
            {
                string scoreIndex = "score"+ i.ToString();
                int score = PlayerPrefs.GetInt(scoreIndex);

                string nameIndex = "name" + i.ToString();
                string name = PlayerPrefs.GetString(nameIndex);

                for (int j = 0; j <= i; j++)
                {
                    if (score > tabScore[j])
                    {
                        for (int k = index; k > j; k--)
                        {
                            tabScore[k] = tabScore[k-1];
                            tabName[k] = tabName[k-1];
                        }

                        tabScore[j] = score;
                        tabName[j] = name;
                        j = i + 1;
                    }
                }
            }
        }

        return (tabScore, tabName);
    }

    private void ResetStoredScore()
    {
        PlayerPrefs.DeleteAll();
    }

    
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 150, 500));
        GUILayout.BeginVertical();

        if (GUILayout.Button("Reset"))
        {
            ResetStoredScore();
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
