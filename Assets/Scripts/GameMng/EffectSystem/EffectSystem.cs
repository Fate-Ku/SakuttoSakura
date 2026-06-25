//
// EffectSystem.cs
// 
// 2026/06/16 Created By Man-Yi, Yeh 
// 2026/06/25 Updated By Fate Ku
// 

using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : IGameSystem
{
    private GameObject m_EffectPrefab;

    private Effect m_Effect;

    // flower materials
    private Dictionary<BlockType, Material> m_Materials;

    public EffectSystem(GameMng gameMng, GameObject effectPrefab,
        Dictionary<BlockType, Material> materials)
        : base(gameMng)
    {
        m_EffectPrefab = effectPrefab;
        m_Materials = materials;
    }

    public override void Init()
    {
        m_Effect = new Effect();
    }

    // combine effect
    public Effect SetCombineEffect(BlockType type, List<Vector2> posList)
    {
        foreach (var pos in posList)
        {
            // 1. create postion
            Vector3 spawnPos = new Vector3(pos.x, pos.y, 2); //behind blocks

            // 2. Instantiate Prefab
            GameObject effectObj = GameObject.Instantiate(
                m_EffectPrefab,
                spawnPos,
                Quaternion.identity
            );

            // 3. get MeshRenderer
            MeshRenderer renderer = effectObj.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                // 4. open MeshRenderer
                renderer.enabled = true;

                // 5. set up materials
                if (m_Materials.ContainsKey(type))
                {
                    renderer.material = m_Materials[type];
                }
            }

            // 6. auto delete objects
            //GameObject.Destroy(effectObj, 2f);
        }

        return m_Effect;
    }




}


