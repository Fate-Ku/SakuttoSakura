//
// EffectSystemConfig.cs
// 
// 2026/06/25 Created By Fate Ku
// 

using UnityEngine;

public class EffectInfo : MonoBehaviour
{
    [Header("Effect Prefab")]
    public GameObject effectPrefab;

    [Header("Materials")]
    public Material matTsubaki;
    public Material matKaede;
    public Material matHimawari;
    public Material matClover;
    public Material matAsagao;
    public Material matKikyou;
    public Material matSakura;

    public GameObject GetEffectPrefab()
    {
        return effectPrefab;
    }

    public Material GetMatTsubaki()
    {
        return matTsubaki;
    }

    public Material GetMatKaede()
    {
        return matKaede;
    }

    public Material GetMatHimawari()
    {
        return matHimawari;
    }

    public Material GetMatClover()
    {
        return matClover;
    }

    public Material GetMatAsagao()
    {
        return matAsagao;
    }

    public Material GetMatKikyou()
    {
        return matKikyou;
    }

    public Material GetMatSakura()
    {
        return matSakura;
    }


}

