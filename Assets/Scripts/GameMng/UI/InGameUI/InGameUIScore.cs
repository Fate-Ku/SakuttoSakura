//
// InGameUIScore.cs
// 
// 2026/06/14 Created By Fate Ku
// 2026/06/23 Updated By Fate Ku
// 2026/06/24 Updated By Man-Yi, Yeh
// 

using TMPro;
using UnityEngine;


public class InGameUIScore
{
    private TextMeshProUGUI m_ScoreText;
    private TextMeshProUGUI m_ComboText;
    private TextMeshPro m_moveableComboText;
    private TextMeshProUGUI m_SakuraText;

    public InGameUIScore(TextMeshProUGUI scoreText, TextMeshProUGUI comboText
        , TextMeshPro moveableComboText, TextMeshProUGUI sakuraText)
    {
        m_ScoreText = scoreText;
        m_ComboText = comboText;
        m_moveableComboText = moveableComboText;
        m_SakuraText = sakuraText;
    }

    public float showComboTime;
    private Vector3 comboStartPos;
    private bool isComboShowing = false;
    private int lastCombo = 0;

    public void Init()
    {
        showComboTime = 0;

        // use in score scene
        if (m_ScoreText != null)
        {
            int score = GameMng.Instance.GetScore();
            m_ScoreText.text = "Score : " + score.ToString();

            int maxCombo = GameMng.Instance.GetMaxCombo();
            m_ComboText.text = "Max Combo : " + maxCombo.ToString();

            if (m_SakuraText != null)
            {
                int sakuraQty = GameMng.Instance.GetBlockDestroyNum(BlockType.Sakura);
                m_SakuraText.text = "Get " + sakuraQty.ToString() + " Sakura";

            }

            //Debug.Log("Score : " + score.ToString());
            //Debug.Log("InGameUIScore Init");
        }
    }

    public void Update()
    {
        if (m_ScoreText != null)
        {
            int score = GameMng.Instance.GetScore();
            m_ScoreText.text = "Score : " + score.ToString();
            Debug.Log("Score : " + score.ToString());

            // combo
            int combo = GameMng.Instance.GetTotalCombo();
            m_ComboText.text = combo.ToString() + " Combo";

            // moveable Combo (position will move)
            if (combo == 0)
            {
                m_moveableComboText.gameObject.SetActive(false);
                isComboShowing = false;
                lastCombo = 0;
                return;
            }

            if (combo != lastCombo)
            {
                lastCombo = combo;

                isComboShowing = true;
                showComboTime = 0;

                Vector2 comboPos = GameMng.Instance.LastDestroyPos;
                comboStartPos = new Vector3(comboPos.x, comboPos.y, -1f);

                m_moveableComboText.gameObject.SetActive(true);
                m_moveableComboText.color = Color.red;
                m_moveableComboText.text = combo.ToString() + " Combo";

                m_moveableComboText.transform.position = comboStartPos;

            }

            if (isComboShowing)
            {
                // animation（1.5s）
                showComboTime += Time.deltaTime;

                float t = showComboTime / 1.5f; // 0 → 1

                // go up 1.5f
                m_moveableComboText.transform.position =
                    comboStartPos + new Vector3(0, t * 1.5f, 0);

                // fade out
                Color c = m_moveableComboText.color;
                c.a = 1f - t;
                m_moveableComboText.color = c;

                // vanish
                if (showComboTime >= 1.5f)
                {
                    m_moveableComboText.gameObject.SetActive(false);
                    isComboShowing = false;
                }

            }

        }
        //Debug.Log("InGameUIScore Update");
    }

    public void Term()
    {
        m_ScoreText = null;

    }

}
