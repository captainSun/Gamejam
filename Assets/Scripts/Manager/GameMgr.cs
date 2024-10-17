using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 游戏管理
/// </summary>
public static class GameMgr
{
    public static GameObject people; //人
    public static GameObject dog; //狗
    public static Canvas canvas; //canvas
    public static Volume globalVolume; //volume组件
    public static Camera mainCamera;
    public static GameObject mainMenu;
    //初始化
    public static void Initialize()
    {
        people = GameObject.FindGameObjectWithTag("People");
        dog = GameObject.FindGameObjectWithTag("Dog");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        globalVolume = GameObject.Find("GlobalVolume").GetComponent<Volume>();
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        globalVolume.enabled = false;
        LoadLoginPanel();
    }

   
    public static void LoadLoginPanel()
    {
        Object uiPrefab = ResourceMgr.LoadResAsset("LoginPanel",AssetsEnum.Prefab);
        GameObject LoginPanel = GameObject.Instantiate(uiPrefab, GameMgr.canvas.transform) as GameObject;
    }

    //开始游戏
    public static void StartGame()
    {
        globalVolume.enabled = true;
        mainCamera.gameObject.SetActive(false);
        Object uiPrefab = ResourceMgr.LoadResAsset("MainMenu",AssetsEnum.Prefab);
        mainMenu = GameObject.Instantiate(uiPrefab, GameMgr.canvas.transform) as GameObject;
    }
}