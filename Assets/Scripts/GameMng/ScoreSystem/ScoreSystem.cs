//
// ScoreSystem.cs
// 
// 2026/06/09 Created By Man-Yi, Yeh
// 2026/06/11 Added By Fate Ku 
// 2026/06/14 Added By Fate Ku 
// 2026/06/23 Added By Fate Ku 
// 

using TMPro;
using UnityEngine;

public class ScoreSystem : IGameSystem
{

    // total socre
    public int TotalScore;
    // total combo qty
    public int TotalCombo;
    public int comboBonus;

    private float lastCallTime = -999f; //inital time

    //private TextMeshProUGUI m_ScoreText;

    //public TextMeshProUGUI TestInGameScoreText
    //{
    //    get { return m_ScoreText; }
    //}

    //-------------------
    //combo
    //-------------------
    private float m_ComboTimer;
    public float ComboTimer
    {
        get { return m_ComboTimer; }
    }
    private int m_ComboBase;
    public int ComboBase
    {
        get { return m_ComboBase; }
    }
    private int m_ComboBaseBonus;
    public int ComboBaseBonus
    {
        get { return m_ComboBaseBonus; }
    }
    private bool m_CanCombo = true;
    public bool CanCombo
    {
        set { m_CanCombo = value; }
    }


    //-------------------
    //Info
    //-------------------
    //score info
    private ScoreInfo m_ScoreInfo;

    public ScoreInfo ScoreInfo
    {
        get { return m_ScoreInfo; }
    }

    public ScoreSystem(GameMng gameMng)
        : base(gameMng)
    {
    }

    public override void Init()
    {
        TotalScore = 0;
        TotalCombo = 0;
        comboBonus = 1;

        //-------------------
        //Info
        //-------------------
        //score info
        GameObject scoreInfo = GameObject.Find("ScoreInfo");
        if (scoreInfo != null)
        {
            m_ScoreInfo = scoreInfo.GetComponent<ScoreInfo>();
        }

        //m_ScoreText = m_ScoreInfo.GetScoreText();
        m_ComboTimer = m_ScoreInfo.GetInComboTime();
        m_ComboBase = m_ScoreInfo.GetComboBase();
        m_ComboBaseBonus = m_ScoreInfo.GetComboBaseBonus();


    }

    public override void Update()
    {

    }

    public override void Term()
    {

    }

    //-------------------------
    //return score to game mgr
    //-------------------------
    public int GetScore()
    {
        return TotalScore;
    }

    public int GetCombo()
    {
        return TotalCombo;
    }


    //-------------------------
    //get bloacktype and num from game mgr
    //-------------------------
    public void SetDestroyInfo(BlockType type, int qty)
    {
        CalculateScoreByFlowerType(type, qty);
        //if (m_ScoreText != null)
        //{
        //    m_ScoreText.text = $"Score : {TotalScore}";
        //}
    }

    private void AddCombo()
    {
        TotalCombo++;
    }

    private void CalculateComboBonus()
    {




    }


    private void CalculateScoreByFlowerType(BlockType type, int qty)
    {
        float now = Time.time;//now

        bool isWithinCanComboSec = (now - lastCallTime) <= m_ComboTimer;

        AddCombo();

        int baseScore = 0;
        int destoryBonus = 0;

        switch (type)
        {
            case BlockType.Tsubaki:
                baseScore = m_ScoreInfo.GetTsubakiScore();
                break;
            case BlockType.Kaede:
                baseScore = m_ScoreInfo.GetKaedeScore();
                break;
            case BlockType.Himawari:
                baseScore = m_ScoreInfo.GetHimawariScore();
                break;
            case BlockType.Clover:
                baseScore = m_ScoreInfo.GetCloverScore();
                break;
            case BlockType.Asagao:
                baseScore = m_ScoreInfo.GetAsagaoScore();
                break;
            case BlockType.Kikyou:
                baseScore = m_ScoreInfo.GetKikyouScore();
                break;
            case BlockType.Sakura:
                baseScore = m_ScoreInfo.GetSakuraScore();
                break;
        }

        switch (qty)
        {
            case 3:
                destoryBonus = m_ScoreInfo.GetDestory3Base();
                break;
            case 4:
                destoryBonus = m_ScoreInfo.GetDestory4Buff();
                break;
            case 5:
                destoryBonus = m_ScoreInfo.GetDestory5Buff();
                break;
            case 6:
                destoryBonus = m_ScoreInfo.GetDestory6Buff();
                break;
            case 7:
                destoryBonus = m_ScoreInfo.GetDestory7Buff();
                break;
            case 8:
                destoryBonus = m_ScoreInfo.GetDestory8Buff();
                break;

        }

        TotalScore += baseScore * destoryBonus * comboBonus;

        Debug.Log($"[ScoreSystem] (block type={type}, block qty={qty}),(baseScore ={baseScore} * destoryBuff = {destoryBonus}) → TotalScore = {TotalScore}");

    }

}
