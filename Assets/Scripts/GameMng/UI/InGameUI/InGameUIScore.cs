//
// InGameUIScore.cs
// 
// 2026/06/14 Created By Fate Ku
// 

using UnityEngine;
using TMPro;

public class InGameUIScore
{
    private TextMeshProUGUI m_ScoreText;
    public InGameUIScore(TextMeshProUGUI scoreText)
    {
        m_ScoreText = scoreText;
    }

    public void Init()
    {

    }

    public void Update()
    {
        if (m_ScoreText != null)
        {
            int score = GameMng.Instance.GetScore();
            m_ScoreText.text = "Score : " + score.ToString();
            Debug.Log("Score : " + score.ToString());
        }
        Debug.Log("InGameUIScore Update");
    }

    public void Term()
    {
        m_ScoreText = null;

    }

}
