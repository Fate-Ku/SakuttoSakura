//
// BlockAnimationTrigger.cs
// 
// 2026/06/16 Created By Man-Yi, Yeh 
// 

using UnityEngine;

public class BlockAnimationTrigger : MonoBehaviour
{
    private IBlock m_Block;

    public void SetBlock(IBlock block)
    {
        m_Block = block;
    }

    public void CallTrigger()
    {
        m_Block.CallStateTrigger();
    }
}
