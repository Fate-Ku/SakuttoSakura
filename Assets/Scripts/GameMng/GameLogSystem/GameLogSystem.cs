//
// GameLogSystem.cs
// 
// 2026/06/09 Created By Man-Yi, Yeh
// 2026/06/11 Added By Fate Ku 
//

using UnityEngine;

public class GameLogSystem : IGameSystem
{
    public GameLogSystem(GameMng gameMng) 
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

    public int GetBlockDestroyNum(BlockType type)
    {
        int res=1;

        return res;
    }

    public void RecordBlockDestroy(BlockType type)
    {

    }

}
