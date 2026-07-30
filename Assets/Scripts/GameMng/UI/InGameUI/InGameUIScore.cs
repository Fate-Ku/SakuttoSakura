//
// InGameUIScore.cs
// 
// 2026/06/14 Created By Fate Ku
// 2026/06/23 Updated By Fate Ku
// 2026/06/24 Updated By Fate Ku
// 2026/06/25 Updated By Fate Ku
// 2026/07/13 Updated By Fate Ku
// 2026/07/17 Updated By Fate Ku
// 2026/07/30 Updated By Fate Ku
// 

using TMPro;
using UnityEngine;

public class InGameUIScore
{
    private TextMeshProUGUI m_ScoreText;
    private TextMeshProUGUI m_ComboText;
    private TextMeshProUGUI m_LevelText;

    private TextMeshPro m_moveableComboPrefab;

    private TextMeshProUGUI m_SakuraText;

    public InGameUIScore(TextMeshProUGUI scoreText, TextMeshProUGUI comboText
        , TextMeshPro moveableComboPrefab, TextMeshProUGUI sakuraText, TextMeshProUGUI levelText)
    {
        m_ScoreText = scoreText;
        m_ComboText = comboText;
        m_moveableComboPrefab = moveableComboPrefab;
        m_SakuraText = sakuraText;
        m_LevelText = levelText;
    }

    public float showComboTime;
    //private Vector3 comboStartPos;
    //private bool isComboShowing = false;
    private int lastCombo = 0;

    public void Init()
    {
        showComboTime = 0;

        // use in score scene
        if (m_ScoreText != null)
        {
            int score = GameMng.Instance.GetScore();
            m_ScoreText.text = score.ToString();

            int maxCombo = GameMng.Instance.GetMaxCombo();
            m_ComboText.text = maxCombo.ToString();

            if (m_LevelText != null)
            {
                int maxLevel = GameMng.Instance.GetMaxLevel();
                m_LevelText.text = maxLevel.ToString();
            }

            if (m_SakuraText != null)
            {
                int sakuraQty = GameMng.Instance.GetBlockDestroyNum(BlockType.Sakura);
                m_SakuraText.text = sakuraQty.ToString();

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
            m_ScoreText.text = score.ToString();
            //Debug.Log("Score : " + score.ToString());

            // combo
            int combo = GameMng.Instance.GetTotalCombo();
            m_ComboText.text = combo.ToString() + " Combo";

            // moveable Combo (position will move)
            if (combo == 0)
            {
                lastCombo = 0;
                return;
            }

            // Combo changed and new words
            if (combo != lastCombo)
            {
                lastCombo = combo;

                Vector2Int blockID = GameMng.Instance.ScoreSystem.LastDestroyBlockID;
                Vector2 pos = GameMng.Instance.GetBgVirtualCubePosition(blockID.x, blockID.y);
                Vector3 spawnPos = new Vector3(pos.x, pos.y, -10f);

                // Create Prefab
                TextMeshPro newComboText = GameObject.Instantiate(
                    m_moveableComboPrefab,
                    spawnPos,
                    Quaternion.identity
                );

                newComboText.text = combo.ToString() + " Combo";
                newComboText.color = Color.red;

                // Animation Script
                newComboText.gameObject.AddComponent<MoveUpFadeOut>();
            }

        }
        //Debug.Log("InGameUIScore Update");
    }

    public void Term()
    {
        m_ScoreText = null;

    }

}
