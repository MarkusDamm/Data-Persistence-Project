using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public string PlayerName;
    private SaveData data;

    private void Awake()
    {
        if (ScoreManager.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        ScoreManager.Instance = this;
    }

    [System.Serializable]
    public class Score
    {
        public string name;
        public int points;
    }

    [System.Serializable]
    class SaveData
    {
        public List<Score> HighScores;
    }

    public void SaveScore(int _points)
    {
        Score playerScore = new Score();
        playerScore.name = PlayerName;
        playerScore.points = _points;

        //SaveData data = new SaveData();
        if (data == null)
        {
            data = new SaveData();
            data.HighScores = new List<Score>(5);
            data.HighScores.Add(playerScore);
        }
        if (data.HighScores == null)
        {
            data.HighScores = new List<Score>(5);
            data.HighScores.Add(playerScore);
            Debug.Log(data);
        }
        else { 
            foreach(Score currentScore in data.HighScores)
            {
                if (_points > currentScore.points)
                {
                    int currentIndex = data.HighScores.IndexOf(currentScore);
                    data.HighScores.Insert(currentIndex, playerScore);
                    Debug.Log(data);
                    break;
                }
            }
        }
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public string LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log(data);
            Debug.Log(data.HighScores);
            string highScoreText = "HighScore: " + data.HighScores[0].name + " with "+ data.HighScores[0].points + " Points!";
            return highScoreText;
        }
        return "No Highscore";
    }


}
