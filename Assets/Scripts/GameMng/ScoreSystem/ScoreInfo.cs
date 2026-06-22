//
// ScoreSystem.cs
// 
// 2026/06/11 Created By Fate Ku 
// 2026/06/14 Added By Fate Ku 
// 

using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ScoreInfo : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField]private Slider TimeSlider;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI MaxSakuraText;
    [SerializeField] private TextMeshProUGUI MaxComboText;

    [Header("Score Setting")]
    [SerializeField] private int TsubakiScore;
    [SerializeField] private int KaedeScore;
    [SerializeField] private int HimawariScore;
    [SerializeField] private int CloverScore;
    [SerializeField] private int AsagaoScore;
    [SerializeField] private int KikyouScore;
    [SerializeField] private int SakuraScore;

    [Header("Plus Buff")]
    [SerializeField] private int Destory3Base;
    [SerializeField] private int Destory4Buff;
    [SerializeField] private int Destory5Buff;
    [SerializeField] private int Destory6Buff;
    [SerializeField] private int Destory7Buff;
    [SerializeField] private int Destory8Buff;

    [Header("Combo Buff")]
    [SerializeField] private int Combo3Base;//bonus start from 3 combo

    public TextMeshProUGUI GetScoreText()
    {
        return ScoreText;
    }

    public Slider GetTimerSlider()
    {
        return TimeSlider;
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
        return Destory4Buff;
    }

    public int GetDestory5Buff()
    {
        return Destory4Buff;
    }

    public int GetDestory6Buff()
    {
        return Destory4Buff;
    }

    public int GetDestory7Buff()
    {
        return Destory4Buff;
    }

    public int GetDestory8Buff()
    {
        return Destory4Buff;
    }

    public int GetCombo3Base()
    {
        return Combo3Base;
    }



}
