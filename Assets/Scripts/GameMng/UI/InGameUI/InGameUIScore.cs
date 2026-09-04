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
// 2026/09/04 Updated By Fate Ku
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

    // Sakura
    private GameObject m_Sakura1;
    private GameObject m_Sakura2;
    private GameObject m_Sakura3;
    private GameObject m_Sakura5;
    private GameObject m_Sakura7;
    private GameObject m_Sakura9;
    private GameObject m_Sakura11;
    private GameObject m_Sakura14;
    private GameObject m_Sakura17;
    private GameObject m_Sakura20;
    private GameObject m_Sakura23;
    private GameObject m_Sakura26;
    private GameObject m_Sakura29;
    private GameObject m_Sakura32;
    private GameObject m_Sakura35;
    private GameObject m_Sakura38;
    private GameObject m_Sakura41;
    private GameObject m_Sakura44;
    private GameObject m_Sakura47;
    private GameObject m_Sakura50;
    private GameObject m_Sakura53;
    private GameObject m_Sakura56;
    private GameObject m_Sakura60;



    public InGameUIScore(TextMeshProUGUI scoreText, TextMeshProUGUI comboText
        , TextMeshPro moveableComboPrefab, TextMeshProUGUI sakuraText, TextMeshProUGUI levelText,
        GameObject NiceTry, GameObject GoodJob, GameObject WellDone,
        GameObject Bronze, GameObject Silver, GameObject Gold,
        GameObject basketRenderer, Transform sakuraTarget,
        GameObject sakura1, GameObject sakura2, GameObject sakura3, GameObject sakura5, GameObject sakura7,
        GameObject sakura9, GameObject sakura11, GameObject sakura14, GameObject sakura17, GameObject sakura20,
        GameObject sakura23, GameObject sakura26, GameObject sakura29, GameObject sakura32, GameObject sakura35,
        GameObject sakura38, GameObject sakura41, GameObject sakura44, GameObject sakura47, GameObject sakura50,
        GameObject sakura53, GameObject sakura56, GameObject sakura60)
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

        m_Sakura1 = sakura1; m_Sakura2 = sakura2; m_Sakura3 = sakura3; m_Sakura5 = sakura5;
        m_Sakura7 = sakura7; m_Sakura9 = sakura9; m_Sakura11 = sakura11; m_Sakura14 = sakura14;
        m_Sakura17 = sakura17; m_Sakura20 = sakura20; m_Sakura23 = sakura23; m_Sakura26 = sakura26;
        m_Sakura29 = sakura29; m_Sakura32 = sakura32; m_Sakura35 = sakura35; m_Sakura38 = sakura38;
        m_Sakura41 = sakura41; m_Sakura44 = sakura44; m_Sakura47 = sakura47; m_Sakura50 = sakura50;
        m_Sakura53 = sakura53; m_Sakura56 = sakura56; m_Sakura60 = sakura60;
    }

    public float showComboTime;
    //private Vector3 comboStartPos;
    //private bool isComboShowing = false;
    private int lastCombo = 0;

    public void Init()
    {
        SakuraInactive();

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

    private void AddSakuraToBasket(int sakuraQty)
    {
        Debug.Log($"Basket Sakura Count = {sakuraQty}");

        if (sakuraQty >= 1)
        {
            m_Sakura1.SetActive(true);
        }
        if (sakuraQty >= 2)
        {
            m_Sakura2.SetActive(true);
        }
        if (sakuraQty >= 3)
        {
            m_Sakura3.SetActive(true);
        }
        if (sakuraQty >= 5)
        {

            m_Sakura5.SetActive(true);
        }
        if (sakuraQty >= 7)
        {

            m_Sakura7.SetActive(true);
        }
        if (sakuraQty >= 9)
        {

            m_Sakura9.SetActive(true);
        }
        if (sakuraQty >= 11)
        {

            m_Sakura11.SetActive(true);
        }
        if (sakuraQty >= 14)
        {

            m_Sakura14.SetActive(true);
        }
        if (sakuraQty >= 17)
        {

            m_Sakura17.SetActive(true);
        }
        if (sakuraQty >= 20)
        {

            m_Sakura20.SetActive(true);
        }
        if (sakuraQty >= 23)
        {

            m_Sakura23.SetActive(true);
        }
        if (sakuraQty >= 26)
        {

            m_Sakura26.SetActive(true);
        }
        if (sakuraQty >= 29)
        {

            m_Sakura29.SetActive(true);
        }
        if (sakuraQty >= 32)
        {

            m_Sakura32.SetActive(true);
        }
        if (sakuraQty >= 35)
        {

            m_Sakura35.SetActive(true);
        }
        if (sakuraQty >= 38)
        {

            m_Sakura38.SetActive(true);
        }
        if (sakuraQty >= 41)
        {

            m_Sakura41.SetActive(true);
        }
        if (sakuraQty >= 44)
        {

            m_Sakura44.SetActive(true);
        }
        if (sakuraQty >= 47)
        {

            m_Sakura47.SetActive(true);
        }
        if (sakuraQty >= 50)
        {

            m_Sakura50.SetActive(true);
        }
        if (sakuraQty >= 53)
        {

            m_Sakura53.SetActive(true);
        }
        if (sakuraQty >= 56)
        {

            m_Sakura56.SetActive(true);
        }
        if (sakuraQty >= 60)
        {

            m_Sakura60.SetActive(true);
        }
    }

    //private void AddSakuraToBasket(int sakuraCount)
    //{
    //    Debug.Log(
    //        $"Add Sakura To Basket Count = {sakuraCount}");

    //    if (m_SakuraRenderer == null)
    //    {
    //        Debug.LogWarning(
    //            "m_SakuraRenderer is null.");
    //        return;
    //    }

    //    if (m_SakuraTarget == null)
    //    {
    //        Debug.LogWarning(
    //            "m_SakuraTarget is null.");
    //        return;
    //    }

    //    // init
    //    foreach (GameObject sakura in m_BasketSakuras)
    //    {
    //        if (sakura != null)
    //        {
    //            GameObject.Destroy(sakura);
    //        }
    //    }

    //    m_BasketSakuras.Clear();

    //    // create sakura
    //    for (int i = 0; i < sakuraCount; i++)
    //    {
    //        GameObject sakura =
    //            GameObject.Instantiate(m_SakuraRenderer);

    //        sakura.name =
    //            $"BasketSakura_{i + 1}";

    //        sakura.transform.localScale =
    //            Vector3.one * BASKET_SAKURA_SCALE;

    //        Vector3 pos =
    //            GetBasketSakuraPosition(i);

    //        sakura.transform.position =
    //            pos;

    //        m_BasketSakuras.Add(sakura);
    //    }

    //    Debug.Log(
    //        $"Created {m_BasketSakuras.Count} Basket Sakuras.");
    //}
    //private Vector3 GetBasketSakuraPosition(int index)
    //{
    //    int column = index % 9;
    //    int row = index / 9;

    //    Vector3 center =
    //        m_SakuraTarget.position;

    //    float startX =
    //        center.x -
    //        (6 * BASKET_SAKURA_SPACING_X * 0.5f);

    //    float x =
    //        startX +
    //        column * BASKET_SAKURA_SPACING_X * 0.8f;

    //    float y =
    //        center.y +
    //        row * BASKET_SAKURA_SPACING_Y * 0.5f;

    //    return new Vector3(
    //        x,
    //        y,
    //        -1f);
    //}


    private void SakuraInactive()
    {
        m_Sakura1.SetActive(false); m_Sakura2.SetActive(false); m_Sakura3.SetActive(false);
        m_Sakura5.SetActive(false); m_Sakura7.SetActive(false); m_Sakura9.SetActive(false);
        m_Sakura11.SetActive(false); m_Sakura14.SetActive(false); m_Sakura17.SetActive(false);
        m_Sakura20.SetActive(false); m_Sakura23.SetActive(false); m_Sakura26.SetActive(false);
        m_Sakura29.SetActive(false); m_Sakura32.SetActive(false); m_Sakura35.SetActive(false);
        m_Sakura38.SetActive(false); m_Sakura41.SetActive(false); m_Sakura44.SetActive(false);
        m_Sakura47.SetActive(false); m_Sakura50.SetActive(false); m_Sakura53.SetActive(false);
        m_Sakura56.SetActive(false); m_Sakura60.SetActive(false);
    }

}
