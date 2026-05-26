//
// InGameSystem.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 

using UnityEngine;

public enum BlockType
{
    //flower
    Tsubaki,
    Kaede,
    Himawari,
    Clover,
    Asagao,
    Kikyou,
    Sakura,

    //obstacle
    Isi,

    //item
    TimeItem,
}

public class InGameSystem : IGameSystem
{
    public InGameSystem(GameMng gameMng) 
        : base(gameMng)
    {
    }

    //frame


    //combine sets



    public override void Init()
    {
        Debug.Log("InGameSystem Init");
    }

    public override void Term()
    {
        Debug.Log("InGameSystem Term");
    }

    public override void Update()
    {
        Debug.Log("InGameSystem Update");
    }
}
