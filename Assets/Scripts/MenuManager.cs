using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    public Text HighScoreText;

    // Start is called before the first frame update
    void Start()
    {
        HighScoreText.text = ScoreManager.Instance.LoadScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (inputField.text != null && inputField.text != "")
        {
            ScoreManager.Instance.PlayerName = inputField.text;
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.LogError("input name");
            return;
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
