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
// 2026/08/03 Updated By Fate Ku
// 2026/08/24 Updated By Fate Ku
// 2026/08/31 Updated By Fate Ku
// 

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUIScore
{
    private TextMeshProUGUI m_ScoreText;
    private TextMeshProUGUI m_ComboText;
    private TextMeshProUGUI m_LevelText;

    private TextMeshPro m_moveableComboPrefab;

    private TextMeshProUGUI m_SakuraText;

    private GameObject m_NiceTry;
    private GameObject m_GoodJob;
    private GameObject m_WellDone;

    private GameObject m_Bronze;
    private GameObject m_Silver;
    private GameObject m_Gold;

    private GameObject m_SakuraRenderer;
    private Transform m_SakuraTarget;


    //====================================
    // Basket Sakura
    //====================================

    private readonly List<GameObject> m_BasketSakuras =
        new List<GameObject>();

    // sakura space
    private const float BASKET_SAKURA_SPACING_X = 0.5f;
    private const float BASKET_SAKURA_SPACING_Y = 0.5f;

    // sakura size
    private const float BASKET_SAKURA_SCALE = 0.35f;



    public InGameUIScore(TextMeshProUGUI scoreText, TextMeshProUGUI comboText
        , TextMeshPro moveableComboPrefab, TextMeshProUGUI sakuraText, TextMeshProUGUI levelText,
        GameObject NiceTry, GameObject GoodJob, GameObject WellDone,
        GameObject Bronze, GameObject Silver, GameObject Gold,
        GameObject basketRenderer, Transform sakuraTarget)
    {
        m_ScoreText = scoreText;
        m_ComboText = comboText;
        m_moveableComboPrefab = moveableComboPrefab;
        m_SakuraText = sakuraText;
        m_LevelText = levelText;

        m_NiceTry = NiceTry;
        m_GoodJob = GoodJob;
        m_WellDone = WellDone;
        m_Bronze = Bronze;
        m_Silver = Silver;
        m_Gold = Gold;

        m_SakuraRenderer = basketRenderer;
        m_SakuraTarget = sakuraTarget;
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

            if (m_SakuraText != null && m_SakuraRenderer != null && m_SakuraTarget != null)
            {
                int sakuraQty = GameMng.Instance.GetBlockDestroyNum(BlockType.Sakura);
                m_SakuraText.text = sakuraQty.ToString();

                AddSakuraToBasket(sakuraQty);
            }

            if (m_NiceTry != null)
            {
                Show();
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

    void HideAll()
    {
        if (m_NiceTry != null)
        {

            m_NiceTry.SetActive(false);
            m_GoodJob.SetActive(false);
            m_WellDone.SetActive(false);

            m_Bronze.SetActive(false);
            m_Silver.SetActive(false);
            m_Gold.SetActive(false);
        }
    }

    public void Show()
    {
        HideAll();

        int score = GameMng.Instance.GetScore();

        if (score < 15000)
        {
            m_NiceTry.SetActive(true);
            m_Bronze.SetActive(true);

        }
        else if (score < 35000)
        {
            m_GoodJob.SetActive(true);
            m_Silver.SetActive(true);

        }
        else
        {
            m_WellDone.SetActive(true);
            m_Gold.SetActive(true);

        }
    }


    private void AddSakuraToBasket(int sakuraCount)
    {
        Debug.Log(
            $"Add Sakura To Basket Count = {sakuraCount}");

        if (m_SakuraRenderer == null)
        {
            Debug.LogWarning(
                "m_SakuraRenderer is null.");
            return;
        }

        if (m_SakuraTarget == null)
        {
            Debug.LogWarning(
                "m_SakuraTarget is null.");
            return;
        }

        // init
        foreach (GameObject sakura in m_BasketSakuras)
        {
            if (sakura != null)
            {
                GameObject.Destroy(sakura);
            }
        }

        m_BasketSakuras.Clear();

        // create sakura
        for (int i = 0; i < sakuraCount; i++)
        {
            GameObject sakura =
                GameObject.Instantiate(m_SakuraRenderer);

            sakura.name =
                $"BasketSakura_{i + 1}";

            sakura.transform.localScale =
                Vector3.one * BASKET_SAKURA_SCALE;

            Vector3 pos =
                GetBasketSakuraPosition(i);

            sakura.transform.position =
                pos;

            m_BasketSakuras.Add(sakura);
        }

        Debug.Log(
            $"Created {m_BasketSakuras.Count} Basket Sakuras.");
    }
    private Vector3 GetBasketSakuraPosition(int index)
    {
        int column = index % 9;
        int row = index / 9;

        Vector3 center =
            m_SakuraTarget.position;

        float startX =
            center.x -
            (6 * BASKET_SAKURA_SPACING_X * 0.5f);

        float x =
            startX +
            column * BASKET_SAKURA_SPACING_X * 0.8f;

        float y =
            center.y +
            row * BASKET_SAKURA_SPACING_Y * 0.5f;

        return new Vector3(
            x,
            y,
            -1f);
    }


}
