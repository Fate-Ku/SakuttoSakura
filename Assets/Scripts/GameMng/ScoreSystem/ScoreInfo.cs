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
    [SerializeField] private int Destory3base = 1;
    [SerializeField] private double Destory4Buff;
    [SerializeField] private double Destory5Buff;
    [SerializeField] private double Destory6Buff;


}
