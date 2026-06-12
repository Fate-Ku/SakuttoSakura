//
// CombineSet.cs
// 
// 2026/06/06 Created By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 2026/06/08 Updated By Man-Yi, Yeh
// 2026/06/12 Updated By Man-Yi, Yeh
//

using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CombineSet
{
    private CombineSetsController m_Controller;

    private BlockType m_Type;
    public BlockType Type
    {
        get { return m_Type; }
    }

    private List<IBlock> m_Blocks = new();
    public List<IBlock> Blocks
    {
        get { return m_Blocks; }
    }

    private bool m_IsStartCombine = false;
    private float m_CombineTimer;
    private int m_CombineSize;

    public CombineSet(
        CombineSetsController controller,
        BlockType type, 
        float combineTimer,
        int combineSize)
    {
        m_Controller = controller;
        m_Type = type;
        m_CombineTimer = combineTimer;
        m_CombineSize = combineSize;
    }

    public void Init(IBlock block1,IBlock block2)
    {
        CombineSet set1 = block1.CombineSet;
        CombineSet set2 = block2.CombineSet;

        if (set1 != null && set1 == set2)
        {
            return;
        }

        //block1 lonely
        if (set1 == null)
        {
            AddToList(block1);
        }
        //block1 not lonely
        else
        {
            foreach (IBlock block in set1.Blocks)
            {
                AddToList(block);
            }
            //remove set1 from controller
            m_Controller.RemoveCombineSet(set1);
        }

        //block2 lonely
        if (set2 == null)
        {
            AddToList(block2);
        }
        //block2 not lonely
        else
        {
            foreach (IBlock block in set2.Blocks)
            {
                AddToList(block);
            }
            //remove set2 from controller
            m_Controller.RemoveCombineSet(set2);
        }
    }

    public void Update()
    {
        if (!m_IsStartCombine) 
        {
            CheckStartCombine();
        }
        else
        {
            CheckEndCombine();
        }
    }

    public void Remove()
    {
        foreach (IBlock block in m_Blocks)
        {
            //remove combine set
            block.CombineSet = null;
        }
        //remove this from controller
        m_Controller.RemoveCombineSet(this);
    }


    //-------------------
    //update method
    //-------------------
    private void CheckStartCombine()
    {
        if (m_Blocks.Count >= m_CombineSize)
        {
            //combinestart
            m_IsStartCombine = true;
            //all blocks go combine
            foreach (IBlock block in m_Blocks)
            {
                block.GoCombine();
            }
        }
    }

    private void CheckEndCombine()
    {
        m_CombineTimer -= Time.deltaTime;

        if (m_CombineTimer <= 0)
        {
            //record combine destroy info
            GameMng.Instance.RecordCombineDestroyInfo(m_Type, m_Blocks.Count);

            //set create block to one block
            int id = 0;
            IBlock firstBlock = m_Blocks[id];
            firstBlock.SetCreateBlock(m_Controller.GetCreateBlock(m_Type));

            //all blocks go destroy
            foreach (IBlock block in m_Blocks)
            {
                //block end combine
                BlockEndCombine(block);
            }

            //remove this from controller
            m_Controller.RemoveCombineSet(this);
        }
    }

    //-------------------
    //basic method
    //-------------------
    private void AddToList(IBlock block)
    {
        if (!m_Blocks.Contains(block))
        {
            m_Blocks.Add(block);
            block.CombineSet = this;
        }
    }

    private void BlockEndCombine(IBlock block)
    {
        //near block near combine
        for (int i = 0; i < (int)BlockNearPos.Count; ++i)
        {
            BlockNode blockNode = block.GetNearNode((BlockNearPos)i);
            blockNode?.Block?.NearDestroy(block);
        }
        //destroy
        block.EndCombine();
    }

}
