//
// GameLogSystem.cs
// 
// 2026/06/09 Created By Man-Yi, Yeh
// 2026/06/11 Added By Fate Ku 
// 2026/06/14 Added By Fate Ku 
//

using System.Collections.Generic;
using UnityEngine;

public class GameLogSystem : IGameSystem
{
    // record BlockType,qty
    private Dictionary<BlockType, int> m_DestroyCount;
    // record max combo
    private int m_MaxCombo;
    // record high score
    private int m_highScore;
    //int inGameScore = GameMng.Instance.GetScore();

    public GameLogSystem(GameMng gameMng)
        : base(gameMng)
    {
    }

    public override void Init()
    {
        m_DestroyCount = new Dictionary<BlockType, int>();

        // initial BlockType = 0
        foreach (BlockType type in System.Enum.GetValues(typeof(BlockType)))
        {
            m_DestroyCount[type] = 0;
        }

        m_MaxCombo = 0;
        m_highScore = 0;
    }

    public override void Update()
    {
        //RecordHighScore(inGameScore);
    }

    public override void Term()
    {

    }

    public int GetBlockDestroyNum(BlockType type)
    {
        return m_DestroyCount.ContainsKey(type) ? m_DestroyCount[type] : 0;
    }

    public void RecordBlockDestroy(BlockType type)
    {
        if (!m_DestroyCount.ContainsKey(type))
            m_DestroyCount[type] = 0;

        ++m_DestroyCount[type];

        ShowQtyByBlockType();
    }

    public void RecordCombo(int combo)
    {
        if (combo > m_MaxCombo)
        {
            m_MaxCombo = combo;
            Debug.Log($"[GameLog] New Max Combo = {m_MaxCombo}");
        }
    }

    public int GetMaxCombo()
    {
        return m_MaxCombo;
    }

    public void RecordHighScore(int score)
    {
        if (score > m_highScore)
        {
            m_highScore = score;
            Debug.Log($"[GameLog] New High Score = {m_highScore}");
        }
    }

    public int GetHighScore()
    {
        return m_highScore;
    }


    public void ShowQtyByBlockType()
    {
        Debug.Log("===== BlockType Destroy Summary =====");

        foreach (var kvp in m_DestroyCount)
        {
            BlockType type = kvp.Key;
            int qty = kvp.Value;

            Debug.Log($"{type} : {qty}");
        }

        Debug.Log("=====================================");
    }


}
