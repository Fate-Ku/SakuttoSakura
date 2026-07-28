//
// RightFall.cs
// 
// 2026/06/24 Created By Man-Yi, Yeh
// 2026/06/25 Updated By Man-Yi, Yeh
// 2026/06/29 Updated By Man-Yi, Yeh
// 2026/07/28 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class RightFall : IFallStrategy
{
    public RightFall(float speed) 
        : base(FallDirection.Right, speed)
    {
    }

    public override void UpdateFall(IBlock block, IBlockFallController controller)
    {
        float moveX = m_Speed * Time.deltaTime;
        float newX = block.Pos.x + moveX;
        float targetX = m_TargetPos.x;
        //test
        block.blockTest.fallTargetX = targetX;

        //check right
        BlockNode rightBlockNode = block.GetNearNode(BlockNearPos.Right);
        if (rightBlockNode != null)
        {
            //if left node has block
            if (rightBlockNode.IsState(BlockNodeState.Occupied))
            {
                IBlock rightBlock = rightBlockNode.Block;
                //if it fall right
                if (rightBlock.IsStateType(BlockStateType.Fall))
                {
                    if (rightBlock.IsFalling(FallDirection.Right))
                    {
                        //if right falling right
                        if (rightBlock.GetFallSpeed() < m_Speed)
                        {
                            float fallBlockX = rightBlock.Pos.x;
                            if (fallBlockX - newX < block.Size)
                            {
                                Debug.Log("test: together start");
                                //set together right that speed as right
                                float speed = rightBlock.GetFallSpeed();
                                block.SetFallController(
                                    new TogetherRightFallController(block, speed, controller.BasicSpeed, m_TargetPos));

                                //move
                                newX = fallBlockX - block.Size;
                                block.SetPos(new Vector2(newX, block.Pos.y));

                                return;
                            }
                        }
                    }
                }
                //if not
                //idle or fall down
                else
                {
                    //reset fall controller
                    controller.ResetlFallController();

                    //back to node pos and go idle
                    block.SetPos(block.BlockNode.Pos);
                    block.GoState(BlockStateType.Idle);

                    return;
                }
            }
        }

        //check arrive
        if (newX >= targetX)
        {
            //if arrive
            //finish fall
            block.GoNearNode(BlockNearPos.Right);

            //move to targetY
            block.SetPos(new Vector2(targetX, block.Pos.y));
            //go next
            controller.GoNextFall = true;
        }
        else
        {
            //move to newY
            block.SetPos(new Vector2(newX, block.Pos.y));
        }

    }

    protected override void SetTargetPos(IBlock block)
    {
        //set target pos as left node's pos
        Vector2 pos = block.GetNearNode(BlockNearPos.Right).Pos;
        m_TargetPos = pos;
    }
}
