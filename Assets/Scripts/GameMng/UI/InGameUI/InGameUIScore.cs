//
// InGameUIScore.cs
// 
// 2026/06/14 Created By Fate Ku
// 2026/06/23 Updated By Fate Ku
// 

using UnityEngine;
using TMPro;

public class InGameUIScore
{
    private TextMeshProUGUI m_ScoreText;
    private TextMeshProUGUI m_ComboText;
    public InGameUIScore(TextMeshProUGUI scoreText, TextMeshProUGUI comboText)
    {
        m_ScoreText = scoreText;
        m_ComboText = comboText;
    }

    public void Init()
    {
        if (m_ScoreText != null)
        {
            int score = GameMng.Instance.GetScore();
            m_ScoreText.text = "Score : " + score.ToString();
            Debug.Log("Score : " + score.ToString());
            Debug.Log("InGameUIScore Init");
        }
    }

    public void Update()
    {
        if (m_ScoreText != null)
        {
            int score = GameMng.Instance.GetScore();
            m_ScoreText.text = "Score : " + score.ToString();
            Debug.Log("Score : " + score.ToString());

            int combo = GameMng.Instance.GetTotalCombo();
            m_ComboText.text = combo.ToString() + " Combo";
        }
        Debug.Log("InGameUIScore Update");
    }

    public void Term()
    {
        m_ScoreText = null;

    }

}
