//
// ScoreSystem.cs
// 
// 2026/06/11 Created By Fate Ku 
// 

using UnityEngine;

public class ScoreInfo : MonoBehaviour
{
    [Header("Score Setting")]
    [SerializeField] private int TsubakiScore;
    [SerializeField] private int KaedeScore;
    [SerializeField] private int HimawariScore;
    [SerializeField] private int CloverScore;
    [SerializeField] private int AsagaoScore;
    [SerializeField] private int KikyouScore;
    [SerializeField] private int SakuraScore;

    [Header("Plus Buff%")]
    [SerializeField] private int Destory3Base = 1;
    [SerializeField] private int Destory4Buff;
    [SerializeField] private int Destory5Buff;
    [SerializeField] private int Destory6Buff;
    [SerializeField] private int Destory7Buff;
    [SerializeField] private int Destory8Buff;

    [Header("Combo Buff%")]
    [SerializeField] private int Combo3Base;//start from 3 combo


}
