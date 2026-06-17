//
// CombineSetsController.cs
// 
// 2026/06/06 Created By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 2026/06/08 Updated By Man-Yi, Yeh
// 2026/06/11 Updated By Man-Yi, Yeh
//


using System;
using System.Collections.Generic;
using UnityEngine;

public class CombineSetsController
{
    //oner
    private InGameSystem m_InGameSystem;

    //sets
    private List<CombineSet> m_Sets = new();

    //gameinfo
    private float m_CombineTimer;
    private int m_CombineSize;

    public CombineSetsController(InGameSystem inGameSystemfloat,float combineTime, int combineSize)
    {
        m_InGameSystem = inGameSystemfloat;
        m_CombineTimer = combineTime;
        m_CombineSize = combineSize;
    }

    public void Update()
    {
        //create list for update
        List<CombineSet> list = new();
        foreach (CombineSet set in m_Sets)
        {
            list.Add(set);
        }
        //update
        foreach(CombineSet set in list)
        {
            set.Update();
        }
    }



    //-------------------
    //method of controller
    //-------------------
    //create new combine set
    public void CreateCombineSet(BlockType type, IBlock block1, IBlock block2)
    {
        //create
        CombineSet set = new(this, type, m_CombineTimer, m_CombineSize);
        set.Init(block1, block2);

        //add to list
        m_Sets.Add(set);
    }

    //remove combine set from list
    public void RemoveCombineSet(CombineSet combineSet)
    {
        //check it's in list
        if (m_Sets.Contains(combineSet))
        {
            //remove from list
            m_Sets.Remove(combineSet);
        }
    }

    //-------------------
    //method of combine set
    //-------------------
    public IBlock GetCreateBlock(BlockType type)
    {
        IBlock block = null;

        if ((int)type >= 0 && (int)type < 6)
        {
            block = m_InGameSystem.CreateBlock((BlockType)((int)type + 1), true);
        }
        
        return block;
    }

}
