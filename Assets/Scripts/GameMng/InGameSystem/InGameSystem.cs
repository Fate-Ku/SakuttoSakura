//
// InGameSystem.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 2026/05/30 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public enum BlockType
{
    None = -1,

    //flower
    Tsubaki,
    Kaede,
    Himawari,
    Clover,
    Asagao,
    Kikyou,
    Sakura,

    //rock
    Ishi,

    //item
    TimeItem,
}

public class InGameSystem : IGameSystem
{
    public InGameSystem(GameMng gameMng) 
        : base(gameMng)
    {
    }

    //gameInfo
    private GameInfo m_GameInfo;

    //frame
    private IBlock testBlock;

    //combine sets



    public override void Init()
    {
        Debug.Log("InGameSystem Init");
        GameObject gameInfo = GameObject.Find("GameInfo");
        if (gameInfo != null)
        {
            m_GameInfo = gameInfo.GetComponent<GameInfo>();
        }

        testBlock = new FlowerBlock(m_GameInfo.GetBlock());
    }

    public override void Term()
    {
        Debug.Log("InGameSystem Term");
    }

    public override void Update()
    {
        Debug.Log("InGameSystem Update");

        if (Input.GetKeyDown(KeyCode.T))
        {
            testBlock.Test(true);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            testBlock.Test(false);
        }
    }
}
