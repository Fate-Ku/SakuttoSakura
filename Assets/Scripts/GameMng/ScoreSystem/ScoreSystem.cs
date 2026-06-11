//
// ScoreSystem.cs
// 
// 2026/06/09 Created By Man-Yi, Yeh
// 2026/06/11 Added By Fate Ku 
// 

using UnityEngine;

public class ScoreSystem : IGameSystem
{
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
    public void GetScore()
    {

    }


    //-------------------------
    //get bloacktype and num from game mgr
    //-------------------------
    public void SetDestroyInfo(BlockType type, int num)
    {

    }

}
