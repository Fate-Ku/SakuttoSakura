//
// EffectSystem.cs
// 
// 2026/06/16 Created By Man-Yi, Yeh 
// 2026/06/25 Updated By Fate Ku
// 2026/07/06 Updated By Fate Ku
// 

using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : IGameSystem
{
    private GameObject m_EffectPrefab;

    private GameObject m_SakuraImagePrefab;
    private Transform m_SakuraTarget;

    // flower materials
    private Dictionary<BlockType, Material> m_Materials;

    // Combine Effects
    private Dictionary<int, CombineEffectData> m_CombineEffects = new Dictionary<int, CombineEffectData>();

    public int m_NextEffectId = 0;

    public EffectSystem(
        GameMng gameMng, GameObject effectPrefab,
        Dictionary<BlockType, Material> materials,
        GameObject sakuraImagePrefab, Transform sakuraTarget)
        : base(gameMng)
    {
        m_EffectPrefab = effectPrefab;
        m_Materials = materials;

        m_SakuraImagePrefab = sakuraImagePrefab;
        m_SakuraTarget = sakuraTarget;
    }

    // combine effect
    public int SetCombineEffect(BlockType type, List<Vector2> posList)
    {
        int id = ++m_NextEffectId;
        CombineEffectData data = new CombineEffectData();
        data.BlockType = type;

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

            // 4. set up materials
            if (renderer != null & m_Materials.ContainsKey(type))
            {
                renderer.material = m_Materials[type];

                //Debug.Log("BlockType = " + type);
                //Debug.Log("Material = " + mat);
                //Debug.Log("Material Name = " + mat.name);
                //renderer.material = mat;

            }
            // save object
            data.EffectObjects.Add(effectObj);
            Debug.Log("Save Object name = " + effectObj.name);
        }
        // save id
        m_CombineEffects[id] = data;

        Debug.Log("Create Effect ID = " + id);

        return id;
    }

    public void OffCombineEffect(int id)
    {
        List<int> removeIds = new List<int>();

        foreach (var pair in m_CombineEffects)
        {
            if (pair.Key <= id)
            {
                BlockType type = pair.Value.BlockType;

                // get first object position
                if (pair.Value.EffectObjects.Count > 0 && type == BlockType.None)
                {
                    SakuraFlyToBasket(pair.Value.EffectObjects[0].transform.position);
                }

                // remove in ID's all objects
                foreach (GameObject obj in pair.Value.EffectObjects)
                {
                    if (obj != null)
                    {
                        GameObject.Destroy(obj);
                    }
                }

                // record which key want to delete
                removeIds.Add(pair.Key);
            }
        }

        // then delete obj in Dictionary
        foreach (int removeId in removeIds)
        {
            m_CombineEffects.Remove(removeId);
            //Debug.Log("Removed Effect ID = " + removeId);
        }
    }


    private void SakuraFlyToBasket(Vector3 startPos)
    {
        if (m_SakuraImagePrefab == null || m_SakuraTarget == null)
            return;

        GameObject sakura =
            GameObject.Instantiate(
                m_SakuraImagePrefab,
                startPos,
                Quaternion.identity);

        SakuraFly fly = sakura.AddComponent<SakuraFly>();

        fly.Init(m_SakuraTarget.position);
    }

}


