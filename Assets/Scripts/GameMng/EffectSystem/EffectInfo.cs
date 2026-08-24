//
// EffectInfo.cs
// 
// 2026/06/25 Created By Fate Ku
// 2026/07/06 Updated By Fate Ku
// 2026/07/07 Updated By Fate Ku
// 2026/07/26 Updated By Fate Ku
// 2026/08/24 Updated By Fate Ku
// 

using UnityEngine;

public class EffectInfo : MonoBehaviour
{
    [Header("Effect Prefab")]
    public GameObject effectPrefab;

    [Header("Create Eff-Materials")]
    public Material matTsubaki;
    public Material matKaede;
    public Material matHimawari;
    public Material matClover;
    public Material matAsagao;
    public Material matKikyou;
    public Material matSakura;

    [Header("Sakura Fly")]
    public GameObject sakuraImagePrefab;
    public Transform sakuraTarget;
    public GameObject sakuraFlyPrefab;
    public GameObject sakuraRenderer;

    // 2026/07/26 added by Fate
    [Header("Destory Eff")]
    public GameObject desEffTsubaki;
    public GameObject desEffKaede;
    public GameObject desEffHimawari;
    public GameObject desEffClover;
    public GameObject desEffAsagao;
    public GameObject desEffKikyou;
    public GameObject desEffSakura;
    // 2026/07/26 added by Fate
    
    public GameObject GetSakuraRenderer()
    {  return sakuraRenderer; }

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

    public GameObject GetSakuraImagePrefab()
    {
        return sakuraImagePrefab;
    }

    public Transform GetSakuraTarget()
    {
        return sakuraTarget;
    }

    public Vector3 GetSakuraTargetPosition()
    {
        return sakuraTarget.position;
    }

    public GameObject GetSakuraFlyPrefab()
    {
        return sakuraFlyPrefab;
    }

    public GameObject GetDesEffTsubaki()
    {
        return desEffTsubaki;
    }

    public GameObject GetDesEffKaede()
    {
        return desEffKaede;
    }

    public GameObject GetDesEffHimawari()
    {
        return desEffHimawari;
    }

    public GameObject GetDesEffClover()
    {
        return desEffClover;
    }
    public GameObject GetDesEffAsagao()
    {
        return desEffAsagao;
    }

    public GameObject GetDesEffKikyou()
    {
        return desEffKikyou;
    }
    public GameObject GetDesEffSakura()
    {
        return desEffSakura;
    }
}

