//
// DownFall.cs
// 
// 2026/06/24 Created By Man-Yi, Yeh
// 

using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LeftFall : IFallStrategy
{
    public LeftFall(float speed) 
        : base(FallDirection.Left, speed)
    {
    }

    public override void UpdateFall(IBlock block, IBlockFallController controller)
    {
        float moveX = m_Speed * Time.deltaTime;
        float newX = block.Pos.x - moveX;
        float targetX = m_TargetPos.x;
        //test
        block.blockTest.fallTargetX = targetX;

        //check left
        BlockNode leftBlockNode = block.GetNearNode(BlockNearPos.Left);
        if (leftBlockNode != null)
        {
            if (leftBlockNode.IsState(BlockNodeState.Occupied))
            {
                IBlock leftBlock = leftBlockNode.Block;
                //check is collision to falling down under
                if (leftBlock.IsStateType(BlockStateType.Fall))
                {
                    if (leftBlock.IsFalling(FallDirection.Left))
                    {
                        if (leftBlock.GetFallSpeed() < m_Speed)
                        {
                            //if under fallind down
                            float fallBlockX = leftBlock.Pos.x;
                            if (newX - fallBlockX < block.Size)
                            {
                                Debug.Log("test: together start");
                                //set together down that speed as under
                                float speed = leftBlock.GetFallSpeed();
                                block.SetFallController(
                                    new TogetherDownFallController(block, speed, controller.BasicSpeed, m_TargetPos));

                                //move
                                newX = fallBlockX + block.Size;
                                block.SetPos(new Vector2(newX, block.Pos.y));

                                return;
                            }
                        }
                    }
                }
            }
        }

        //check arrive
        if (newX <= targetX)
        {
            //if arrive
            //finish fall
            block.GoNearNode(BlockNearPos.Left);

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
        Vector2 pos = block.GetNearNode(BlockNearPos.Left).Pos;
        m_TargetPos = pos;
    }
}
