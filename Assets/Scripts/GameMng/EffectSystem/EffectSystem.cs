//
// EffectSystem.cs
// 
// 2026/06/16 Created By Man-Yi, Yeh 
// 2026/06/25 Updated By Fate Ku
// 2026/07/06 Updated By Fate Ku
// 2026/07/10 Updated By Fate Ku
// 2026/07/11 Updated By Fate Ku
// 2026/07/13 Updated By Fate Ku
// 2026/07/26 Updated By Fate Ku
// 2026/08/24 Updated By Fate Ku
// 2026/08/31 Updated By Fate Ku
// 2026/09/04 Updated By Fate Ku
// 

using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : IGameSystem
{
    private GameObject m_EffectPrefab;

    private GameObject m_SakuraImagePrefab;
    private Transform m_SakuraTarget;
    private GameObject m_SakuraFlyPrefab;
    private GameObject m_SakuraRenderer;

    // flower materials
    private Dictionary<BlockType, Material> m_Materials;

    // Destroy Effects
    private Dictionary<BlockType, GameObject> m_DesEff;

    // Combine Effects 
    private Dictionary<int, CombineEffectData> m_CombineEffects = new Dictionary<int, CombineEffectData>();

    // Destroy Effects 
    private Dictionary<int, DestroyEffectData> m_DestroyEffects = new Dictionary<int, DestroyEffectData>();

    public int m_NextCombineEffectId = 0;
    public int m_NextDestroyEffectId = 0;

    //====================================
    // Basket Sakura
    //====================================

    private readonly List<GameObject> m_BasketSakuras =
        new List<GameObject>();

    private int m_BasketSakuraCount = 0;

    // sakura space
    private const float BASKET_SAKURA_SPACING_X = 0.5f;
    private const float BASKET_SAKURA_SPACING_Y = 0.5f;

    // sakura size
    private const float BASKET_SAKURA_SCALE = 0.35f;

    // Sakura
    private GameObject m_Sakura1;
    private GameObject m_Sakura2;
    private GameObject m_Sakura3;
    private GameObject m_Sakura5;
    private GameObject m_Sakura7;
    private GameObject m_Sakura9;
    private GameObject m_Sakura11;
    private GameObject m_Sakura14;
    private GameObject m_Sakura17;
    private GameObject m_Sakura20;
    private GameObject m_Sakura23;
    private GameObject m_Sakura26;
    private GameObject m_Sakura29;
    private GameObject m_Sakura32;
    private GameObject m_Sakura35;
    private GameObject m_Sakura38;
    private GameObject m_Sakura41;
    private GameObject m_Sakura44;
    private GameObject m_Sakura47;
    private GameObject m_Sakura50;
    private GameObject m_Sakura53;
    private GameObject m_Sakura56;
    private GameObject m_Sakura60;

    public EffectSystem(
        GameMng gameMng, GameObject effectPrefab,
        Dictionary<BlockType, Material> materials,
        GameObject sakuraImagePrefab, Transform sakuraTarget,
        GameObject sakuraFlyPrefab, Dictionary<BlockType, GameObject> desEff, GameObject basketRenderer,
        GameObject sakura1, GameObject sakura2, GameObject sakura3, GameObject sakura5, GameObject sakura7,
        GameObject sakura9, GameObject sakura11, GameObject sakura14, GameObject sakura17, GameObject sakura20,
        GameObject sakura23, GameObject sakura26, GameObject sakura29, GameObject sakura32, GameObject sakura35,
        GameObject sakura38, GameObject sakura41, GameObject sakura44, GameObject sakura47, GameObject sakura50,
        GameObject sakura53, GameObject sakura56, GameObject sakura60)
        : base(gameMng)
    {
        m_EffectPrefab = effectPrefab;
        m_Materials = materials;

        m_SakuraImagePrefab = sakuraImagePrefab;
        m_SakuraTarget = sakuraTarget;
        m_SakuraFlyPrefab = sakuraFlyPrefab;

        m_DesEff = desEff;

        m_SakuraRenderer = basketRenderer;

        m_Sakura1 = sakura1; m_Sakura2 = sakura2; m_Sakura3 = sakura3; m_Sakura5 = sakura5;
        m_Sakura7 = sakura7; m_Sakura9 = sakura9; m_Sakura11 = sakura11; m_Sakura14 = sakura14;
        m_Sakura17 = sakura17; m_Sakura20 = sakura20; m_Sakura23 = sakura23; m_Sakura26 = sakura26;
        m_Sakura29 = sakura29; m_Sakura32 = sakura32; m_Sakura35 = sakura35; m_Sakura38 = sakura38;
        m_Sakura41 = sakura41; m_Sakura44 = sakura44; m_Sakura47 = sakura47; m_Sakura50 = sakura50;
        m_Sakura53 = sakura53; m_Sakura56 = sakura56; m_Sakura60 = sakura60;

    }

    public override void Init()
    {
        m_Sakura1.SetActive(false); m_Sakura2.SetActive(false); m_Sakura3.SetActive(false);
        m_Sakura5.SetActive(false); m_Sakura7.SetActive(false); m_Sakura9.SetActive(false);
        m_Sakura11.SetActive(false); m_Sakura14.SetActive(false); m_Sakura17.SetActive(false);
        m_Sakura20.SetActive(false); m_Sakura23.SetActive(false); m_Sakura26.SetActive(false);
        m_Sakura29.SetActive(false); m_Sakura32.SetActive(false); m_Sakura35.SetActive(false);
        m_Sakura38.SetActive(false); m_Sakura41.SetActive(false); m_Sakura44.SetActive(false);
        m_Sakura47.SetActive(false); m_Sakura50.SetActive(false); m_Sakura53.SetActive(false);
        m_Sakura56.SetActive(false); m_Sakura60.SetActive(false);
    }


    // combine effect
    public int SetCombineEffect(BlockType type, List<Vector2> posList)
    {
        int id = ++m_NextCombineEffectId;
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
                //BlockType type = pair.Value.BlockType;

                //// get first object position
                //if (pair.Value.EffectObjects.Count > 0 && type == BlockType.None)
                //{
                //    SakuraFlyToBasket(pair.Value.EffectObjects[0].transform.position);
                //}

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

    // destroy effect
    public int SetDestroyEffect(BlockType type, Vector2Int blockID)
    {
        int id = ++m_NextDestroyEffectId;

        DestroyEffectData data = new DestroyEffectData();

        data.BlockType = type;
        data.BlockID = blockID;

        // get bg position
        data.Position = GameMng.Instance.GetBgVirtualCubePosition(blockID.x, blockID.y);

        Vector3 spawnPos = new Vector3(data.Position.x, data.Position.y, -5f);

        Debug.Log($"SetDestroyEffect Type = {type}");
        // Create Destroy Effect
        if (m_DesEff != null && m_DesEff.ContainsKey(type))
        {
            Debug.Log("Found Destroy Effect");
            GameObject prefab = m_DesEff[type];

            if (prefab != null)
            {
                Debug.Log($"Instantiate {prefab.name}");
                GameObject effectObj = GameObject.Instantiate(
                    prefab,
                    spawnPos,
                    Quaternion.identity);

                data.EffectObjects.Add(effectObj);
            }
        }
        else
        {
            Debug.LogWarning($"Can't find DestroyEffect : {type}");
        }


        // save to  Dictionary
        m_DestroyEffects[id] = data;

        Debug.Log(
            $"Create DestroyEffect ID={id}, " +
            $"Type={type}, " +
            $"BlockID={blockID}, " +
            $"Pos={data.Position}");

        return id;
    }

    public void OffDestroyEffect(int effectID)
    {
        List<int> removeIds = new List<int>();

        foreach (var pair in m_DestroyEffects)
        {
            if (pair.Key > effectID)
                continue;

            // Delete Destroy Effect
            foreach (GameObject obj in pair.Value.EffectObjects)
            {
                if (obj != null)
                    GameObject.Destroy(obj);
            }

            // Sakura Effect
            if (pair.Value.BlockType == BlockType.Sakura)
            {
                SakuraFlyToBasket(pair.Value.Position);
            }

            removeIds.Add(pair.Key);
        }

        foreach (int id in removeIds)
        {
            m_DestroyEffects.Remove(id);
        }
    }


    private void SakuraFlyToBasket(Vector3 startPos)
    {
        if (m_SakuraImagePrefab == null || m_SakuraTarget == null)
            return;

        Vector3 spawnPos = new Vector3(startPos.x, startPos.y, -5f);

        // main picture
        GameObject sakura = GameObject.Instantiate(
            m_SakuraImagePrefab,
            spawnPos,
            Quaternion.identity);

        // Effect
        if (m_SakuraFlyPrefab != null)
        {
            GameObject flyEffect = GameObject.Instantiate(
                m_SakuraFlyPrefab,
                sakura.transform);

            //flyEffect.transform.localPosition = Vector3.zero;
            flyEffect.transform.localRotation = Quaternion.identity;
            flyEffect.transform.localScale = Vector3.one;

            // Particle follow with father object
            ParticleSystem ps = flyEffect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var main = ps.main;
                main.simulationSpace = ParticleSystemSimulationSpace.Local;
            }
        }

        SakuraFly fly = sakura.AddComponent<SakuraFly>();

        fly.Init(
            m_SakuraTarget.position,
            AddSakuraToBasket
        );
    }


    private void AddSakuraToBasket()
    {
        m_BasketSakuraCount++;

        Debug.Log($"Basket Sakura Count = {m_BasketSakuraCount}");

        if (m_BasketSakuraCount >= 1)
        {
            m_Sakura1.SetActive(true);
        }
        if (m_BasketSakuraCount >= 2)
        {
            m_Sakura2.SetActive(true);
        }
        if (m_BasketSakuraCount >= 3)
        {
            m_Sakura3.SetActive(true);
        }
        if (m_BasketSakuraCount >= 5)
        {

            m_Sakura5.SetActive(true);
        }
        if (m_BasketSakuraCount >= 7)
        {

            m_Sakura7.SetActive(true);
        }
        if (m_BasketSakuraCount >= 9)
        {

            m_Sakura9.SetActive(true);
        }
        if (m_BasketSakuraCount >= 11)
        {

            m_Sakura11.SetActive(true);
        }
        if (m_BasketSakuraCount >= 14)
        {

            m_Sakura14.SetActive(true);
        }
        if (m_BasketSakuraCount >= 17)
        {

            m_Sakura17.SetActive(true);
        }
        if (m_BasketSakuraCount >= 20)
        {

            m_Sakura20.SetActive(true);
        }
        if (m_BasketSakuraCount >= 23)
        {

            m_Sakura23.SetActive(true);
        }
        if (m_BasketSakuraCount >= 26)
        {

            m_Sakura26.SetActive(true);
        }
        if (m_BasketSakuraCount >= 29)
        {

            m_Sakura29.SetActive(true);
        }
        if (m_BasketSakuraCount >= 32)
        {

            m_Sakura32.SetActive(true);
        }
        if (m_BasketSakuraCount >= 35)
        {

            m_Sakura35.SetActive(true);
        }
        if (m_BasketSakuraCount >= 38)
        {

            m_Sakura38.SetActive(true);
        }
        if (m_BasketSakuraCount >= 41)
        {

            m_Sakura41.SetActive(true);
        }
        if (m_BasketSakuraCount >= 44)
        {

            m_Sakura44.SetActive(true);
        }
        if (m_BasketSakuraCount >= 47)
        {

            m_Sakura47.SetActive(true);
        }
        if (m_BasketSakuraCount >= 50)
        {

            m_Sakura50.SetActive(true);
        }
        if (m_BasketSakuraCount >= 53)
        {

            m_Sakura53.SetActive(true);
        }
        if (m_BasketSakuraCount >= 56)
        {

            m_Sakura56.SetActive(true);
        }
        if (m_BasketSakuraCount >= 60)
        {

            m_Sakura60.SetActive(true);
        }
    }


    //private void AddSakuraToBasket()
    //{
    //    m_BasketSakuraCount++;

    //    Debug.Log(
    //        $"Basket Sakura Count = {m_BasketSakuraCount}");

    //    if (m_SakuraRenderer == null)
    //        return;

    //    GameObject sakura =
    //        GameObject.Instantiate(m_SakuraRenderer);

    //    sakura.name =
    //        $"BasketSakura_{m_BasketSakuraCount}";

    //    sakura.transform.localScale =
    //        Vector3.one * BASKET_SAKURA_SCALE;

    //    Vector3 pos =
    //        GetBasketSakuraPosition(
    //            m_BasketSakuraCount - 1);

    //    sakura.transform.position = pos;

    //    m_BasketSakuras.Add(sakura);
    //}

    //private Vector3 GetBasketSakuraPosition(int index)
    //{
    //    int column = index % 9;
    //    int row = index / 9;

    //    Vector3 center =
    //        m_SakuraTarget.position;

    //    float startX =
    //        center.x -
    //        (6 * BASKET_SAKURA_SPACING_X * 0.5f);

    //    float x =
    //        startX +
    //        column * BASKET_SAKURA_SPACING_X * 0.8f;

    //    float y =
    //        center.y +
    //        row * BASKET_SAKURA_SPACING_Y * 0.5f;

    //    return new Vector3(
    //        x,
    //        y,
    //        -1f);
    //}
}


