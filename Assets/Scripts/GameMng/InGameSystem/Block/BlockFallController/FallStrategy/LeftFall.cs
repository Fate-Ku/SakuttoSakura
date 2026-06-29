//
// LeftFall.cs
// 
// 2026/06/24 Created By Man-Yi, Yeh
// 2026/06/25 Updated By Man-Yi, Yeh
// 2026/06/29 Updated By Man-Yi, Yeh
// 

using UnityEngine;

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
            //if left node has block
            if (leftBlockNode.IsState(BlockNodeState.Occupied))
            {
                IBlock leftBlock = leftBlockNode.Block;
                //if it fall left
                if (leftBlock.IsFalling(FallDirection.Left))
                {
                    //if under falling down
                    if (leftBlock.GetFallSpeed() < m_Speed)
                    {
                        float fallBlockX = leftBlock.Pos.x;
                        if (newX - fallBlockX < block.Size)
                        {
                            Debug.Log("test: together left start");
                            //set together left that speed as left
                            float speed = leftBlock.GetFallSpeed();
                            block.SetFallController(
                                new TogetherLeftFallController(block, speed, controller.BasicSpeed, m_TargetPos));

                            //move
                            newX = fallBlockX + block.Size;
                            block.SetPos(new Vector2(newX, block.Pos.y));
                            return;
                        }
                    }
                }
                //if not
                //idle or fall down
                else
                {
                    //back to node pos and go idle
                    block.SetPos(block.BlockNode.Pos);
                    block.GoState(BlockStateType.Idle);

                    //reset fall controller
                    controller.ResetlFallController();

                    return;
                }
            }
            //if left node don't has block
            else
            {
                //check left's left
                BlockNode leftLeftBlockNode = leftBlockNode.GetNearNode(BlockNearPos.Left);
                if (leftLeftBlockNode != null) 
                {
                    //if left's left node has block
                    if (leftLeftBlockNode.IsState(BlockNodeState.Occupied))
                    {
                        IBlock leftLeftBlock = leftLeftBlockNode.Block;
                        ///if left's left block id fall right
                        if (leftLeftBlock.IsFalling(FallDirection.Right))
                        {
                            float fallBlockX = leftLeftBlock.Pos.x;
                            if (newX - fallBlockX < block.Size)
                            {
                                Debug.Log("test: together right start");
                                //back to now node
                                //set together right that speed as left
                                float speed = leftLeftBlock.GetFallSpeed();
                                Vector2 targetPos = block.BlockNode.Pos;
                                block.SetFallController(
                                    new TogetherRightFallController(block, speed, controller.BasicSpeed, targetPos));
                                //go left node
                                //start fall right
                                block.GoNearNode(BlockNearPos.Left);
                                block.StartFall(FallDirection.Left);

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
