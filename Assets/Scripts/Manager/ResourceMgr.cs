using System.Collections.Generic;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEngine;

//资源类型定义
public enum AssetsEnum
{
    Audio,
    Prefab,
    Texture,
    Material,
}

/// <summary>
/// 资源加载管理
/// 通过Resources文件夹加载资源
/// </summary>
public static class ResourceMgr
{
    //已加载资源容器
    public static List<AssetDataInfo> dicAllAsset = new List<AssetDataInfo>();
    public class AssetDataInfo
    {
        public string assetName;
        public AssetsEnum assetType;
        public Object assetObj;
    }
   
    //资源类型对应文件夹路径
    public static readonly string AudioRootPath = "Audios/";
    public static readonly string PrefabRootPath = "Prefabs/";
    public static readonly string TextureRootPath = "Textures/";
    public static readonly string MaterialRootPath = "Materials/";
    
    //指定类型加载
    public static T LoadResAsset<T>(string assetName, AssetsEnum assetsEnum) where T : Object
    {
        AssetDataInfo fdata = dicAllAsset.Find(x => x.assetName == assetName);
        if (fdata != null)
        {
            return (T)fdata.assetObj;
        }
        string path = GetResPath(assetName, assetsEnum) ;
   
        T loadAsset = Resources.Load<T>(path);
        //无效资源判断,null 或值类型判断
        if (loadAsset == default(T))
        {
            Debug.LogWarning("加载的资源不存在assetName=" + assetName);
            return default(T);
        }
        AssetDataInfo dataInfo = new AssetDataInfo();
        dataInfo.assetName = assetName;
        dataInfo.assetType = assetsEnum;
        dataInfo.assetObj = loadAsset;
        dicAllAsset.Add(dataInfo);
        return loadAsset;
    }
    
    //不指定类型加载
    public static Object LoadResAsset(string assetName, AssetsEnum assetsEnum, System.Type systemTypeInstance = null)
    {
        Object asset = null;
        AssetDataInfo fdata = dicAllAsset.Find(x => x.assetName == assetName);
        if (fdata != null)
        {
            return fdata.assetObj;
        }
        string path = GetResPath(assetName, assetsEnum);
        Debug.Log("path=  "+ path);
        if (systemTypeInstance == null)
        {
            asset = Resources.Load(path);
        }
        else
        {
            asset = Resources.Load(path, systemTypeInstance);
        }

        if (asset != null)
        {
            AssetDataInfo dataInfo = new AssetDataInfo();
            dataInfo.assetName = assetName;
            dataInfo.assetType = assetsEnum;
            dataInfo.assetObj = asset;
            dicAllAsset.Add(dataInfo);
        }
        else
        {
            Debug.LogWarning("加载的资源为空assetName=" + assetName);
        }
        return asset;
    }

    //获取资源路径
    static string GetResPath(string assetPath, AssetsEnum assetsEnum)
    {
        string path = "";
        switch (assetsEnum)
        {
            case AssetsEnum.Audio:
                path = AudioRootPath + assetPath;
                break;
            case AssetsEnum.Prefab:
                path = PrefabRootPath + assetPath;
                break;
            case AssetsEnum.Texture:
                path = TextureRootPath + assetPath;
                break;
            case AssetsEnum.Material:
                path = MaterialRootPath + assetPath;
                break;
        }
        return path;
    }

    /// <summary>
    /// 根据路径实例化
    /// </summary>
    /// <param name="path"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static GameObject CreateObj(string path, Transform parent)
    {
        Object uiPrefab = ResourceMgr.LoadResAsset(path,AssetsEnum.Prefab);
        GameObject obj = GameObject.Instantiate(uiPrefab, parent) as GameObject;
        return obj;
    }

    //获取所有的图片
    public static Sprite[] GetAllSprite(string assetName, AssetsEnum assetsEnum)
    {
        string path = GetResPath(assetName, assetsEnum);
        Sprite[] allSp = Resources.LoadAll<Sprite>(path);
        return allSp;
    }
    
    //销毁对象
    public static void DestroyObj(GameObject obj, float delay = 0, bool bImmediate = false)
    {
        if (null != obj)
        {
            if (false == bImmediate)
            {
                if (delay > 0)
                {
                    GameObject.Destroy(obj, delay);
                }
                else
                {
                    GameObject.Destroy(obj); 
                }
                
            }
            else
            {
                GameObject.DestroyImmediate(obj);
            }
        }
    }
    //清理某资源
    public static void ClearAsset(string assetName)
    {
        AssetDataInfo fdata = dicAllAsset.Find(x => x.assetName == assetName);
        if (fdata != null)
        {
            dicAllAsset.Remove(fdata);
        }
    }
    
    //清理并卸载对应类型的资源
    public static void ClearAsset(AssetsEnum type)
    {
        for (int i = dicAllAsset.Count-1; i >0; i--)
        {
            if (dicAllAsset[i].assetType == type)
            {
                dicAllAsset.RemoveAt(i);
            }
        }
        Resources.UnloadUnusedAssets();
    }
    
    //清理并卸载所有资源
    public static void ClearAllAsset()
    {
        dicAllAsset.Clear();
        Resources.UnloadUnusedAssets();
    }
    
    //获取已加载的资源数量
    public static int GetAssetCount()
    {
        return dicAllAsset.Count;
    }

}

