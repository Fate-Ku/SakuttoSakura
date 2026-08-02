//
// ScoreInfo.cs
// 
// 2026/06/11 Created By Fate Ku 
// 2026/06/14 Added By Fate Ku 
// 2026/06/23 Added By Fate Ku 
// 2026/06/24 Added By Fate Ku 
// 2026/07/17 Added By Fate Ku 
// 2026/07/27 Added By Fate Ku 
// 2026/08/03 Added By Fate Ku 
// 

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ScoreInfo : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private Slider TimeSlider;
    [SerializeField] private TextMeshProUGUI TimeText;

    [Header("Show Result")]
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI SakuraText;
    [SerializeField] private TextMeshProUGUI ComboText;
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private TextMeshPro MoveableComboText;

    [Header("Show Result- words")]
    [SerializeField] public GameObject NiceTry;
    [SerializeField] public GameObject GoodJob;
    [SerializeField] public GameObject Welldone;

    [Header("Show Result- stamps")]
    [SerializeField] public GameObject BronzeStamp;
    [SerializeField] public GameObject SilverStamp;
    [SerializeField] public GameObject GoldStamp;

    [Header("Show Game State")]
    [SerializeField] private TextMeshPro InGameStateLevelText;
    [SerializeField] public GameObject GameStart;
    [SerializeField] public GameObject TimeUp;
    [SerializeField] public GameObject GameOver;
    [SerializeField] public GameObject Level;


    [Header("Base Score")]
    [SerializeField] private int TsubakiScore;
    [SerializeField] private int KaedeScore;
    [SerializeField] private int HimawariScore;
    [SerializeField] private int CloverScore;
    [SerializeField] private int AsagaoScore;
    [SerializeField] private int KikyouScore;
    [SerializeField] private int SakuraScore;

    [Header("Destory Bonus")]
    [SerializeField] private int Destory3Base;
    [SerializeField] private int Destory4Bonus;
    [SerializeField] private int Destory5Bonus;
    [SerializeField] private int Destory6Bonus;
    [SerializeField] private int Destory7Bonus;
    [SerializeField] private int Destory8Bonus;

    [Header("Combo Bonus")]
    [SerializeField] private int CanComboTime;
    //[SerializeField] private int ShowComboTime;
    [SerializeField] private int ComboBase;//base combo
    [SerializeField] private int ComboBaseBonus;//bonus

    public TextMeshProUGUI GetScoreText()
    {
        return ScoreText;
    }

    public Slider GetTimerSlider()
    {
        return TimeSlider;
    }

    public TextMeshProUGUI GetTimeText()
    {
        return TimeText;
    }

    public TextMeshProUGUI GetComboText()
    {
        return ComboText;
    }

    public TextMeshProUGUI GetLevelText()
    {
        return LevelText;
    }

    public TextMeshPro GetMoveableComboText()
    {
        return MoveableComboText;
    }

    public TextMeshProUGUI GetSakuraText()
    {
        return SakuraText;
    }

    public TextMeshPro GetInGameStateLevelText()
    {
        return InGameStateLevelText;
    }

    public int GetTsubakiScore()
    {
        return TsubakiScore;
    }

    public int GetKaedeScore()
    {
        return KaedeScore;
    }
    public int GetHimawariScore()
    {
        return HimawariScore;
    }

    public int GetCloverScore()
    {
        return CloverScore;
    }

    public int GetAsagaoScore()
    {
        return AsagaoScore;
    }
    public int GetKikyouScore()
    {
        return KikyouScore;
    }

    public int GetSakuraScore()
    {
        return SakuraScore;
    }

    public int GetDestory3Base()
    {
        return Destory3Base;
    }

    public int GetDestory4Buff()
    {
        return Destory4Bonus;
    }

    public int GetDestory5Buff()
    {
        return Destory5Bonus;
    }

    public int GetDestory6Buff()
    {
        return Destory6Bonus;
    }

    public int GetDestory7Buff()
    {
        return Destory7Bonus;
    }

    public int GetDestory8Buff()
    {
        return Destory8Bonus;
    }

    public int GetComboBase()
    {
        return ComboBase;
    }

    public int GetInComboTime()
    {
        return CanComboTime;
    }

    //public int GetShowComboTime()
    //{
    //    return ShowComboTime;
    //}

    public int GetComboBaseBonus()
    {
        return ComboBaseBonus;
    }

    public GameObject GetGameStart()
    {
        return GameStart;
    }

    public GameObject GetGameOver() { return GameOver; }

    public GameObject GetTimeUp() { return TimeUp; }

    public GameObject GetLevel() { return Level; }

    public GameObject GetNiceTry() { return NiceTry; }

    public GameObject GetGoodJob() { return GoodJob; }

    public GameObject GetWelldone() { return Welldone; }

    public GameObject GetBronzeStamp() { return BronzeStamp; }

    public GameObject GetSilverStamp() { return SilverStamp; }
    public GameObject GetGoldStamp() { return GoldStamp; }

}
