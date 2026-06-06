//
// CombineSetsController.cs
// 
// 2026/06/06 Created By Man-Yi, Yeh
//


using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CombineSetsController
{
    //sets
    private List<CombineSet> m_Sets = new();

    //gameinfo
    private float m_CombineTimer;
    private int m_CombineSize;

    public CombineSetsController(float combineTime, int combineSize)
    {
        m_CombineTimer = combineTime;
        m_CombineSize = combineSize;
    }

    public void Update()
    {
        foreach (CombineSet set in m_Sets)
        {
            set.Update();
        }
    }

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

}
