using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Sequence = DG.Tweening.Sequence;

/// <summary>
/// 游戏管理
/// </summary>
public static class GameMgr
{
    // private static bool inEditor = true;
    
    public static bool firstRound = true; //是否是第一轮游戏 用于判断动画是否播放
    public static GameObject Environment; //相关设置根节点
    public static GameObject people; //人
    public static GameObject dog; //狗
    public static GameObject rope;//绳子
    public static MainMenu mainMenu;
    public static Canvas canvas; //canvas
    public static Volume globalVolume; //volume组件
    public static Camera mainCamera;
    public static GameObject dialogueUGUI;

    private static GameObject openPageAni;
  
        
    //初始化
    public static void Initialize()
    {
        Environment = GameObject.Find("Environment");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        dialogueUGUI = GameObject.Find("@DialogueUGUI");
        globalVolume = Environment.transform.Find("GlobalVolume").GetComponent<Volume>();
        people = GameObject.FindGameObjectWithTag("People");
        dog = GameObject.FindGameObjectWithTag("Dog");
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        globalVolume.gameObject.SetActive(true);
        globalVolume.enabled = false;

        people.SetActive(false);
        dog.SetActive(false);
        LoadLoginPanel();
    }

   
    public static void LoadLoginPanel()
    {
        ResetGameObj();
        ResourceMgr.CreateObj("LoginPanel", canvas.transform);
        EventMgr.AddEvent("OpenAniEnd", StartGame);
    }

    //开始游戏
    public static void StartGame()
    {
        GameObject.DestroyImmediate(openPageAni);
        globalVolume.enabled = true;
        mainCamera.gameObject.SetActive(false);
        
        LevelController.ActiveTrigger(true);
        people.SetActive(true);
        dog.SetActive(true);
        
        mainMenu = ResourceMgr.CreateObj("MainMenu", canvas.transform).GetComponent<MainMenu>();
        LevelController.StartLevel(1);
    }

    public static void ResetGame()
    {
        globalVolume.enabled = false;
        mainCamera.gameObject.SetActive(true);
        ResourceMgr.DestroyObj(mainMenu.gameObject);
        LoadLoginPanel();
        firstRound = false;
    }

    //开关移动组件
    public static void SetMoveControl(bool flag)
    {
        people.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        people.GetComponent<PeopleMoveController>().enabled = flag;
        dog.GetComponent<POLYGON_DogAnimationController>().enabled = flag;
        dialogueUGUI.SetActive(true);
    }
    
    public static void ResetGameObj()
    {
        SetMoveControl(false);
        people.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        dialogueUGUI.SetActive(false);
    }
    
    //播放开场动画
    public static void PlayOpen()
    {
        if (firstRound == false)
        {
            StartGame();
        }
        else
        {
            openPageAni = ResourceMgr.CreateObj("openPageAni", canvas.transform);
        }
        
    }
    
    //播放摔倒过场
    public static void PlayFall(Action action)
    {
        ResetGameObj();
        var ani = ResourceMgr.CreateObj("StoryPageAni", canvas.transform);
        ani.GetComponent<StoryPageAni>().PlayAni("fall", action);
    }
    
    //播放中间过场
    public static void PlayBridge(Action action)
    {
        ResetGameObj();
        var ani = ResourceMgr.CreateObj("StoryPageAni", canvas.transform);
        ani.GetComponent<StoryPageAni>().PlayAni("bridge", action);
    }
    
    //播放失败过场
    public static void PlayDefeat(Action action, string reason)
    {
        ResetGameObj();
        var ani = ResourceMgr.CreateObj("StoryPageAni", canvas.transform);
        ani.GetComponent<StoryPageAni>().PlayAni(reason, () =>
        {
            ResetGame();
            action.Invoke();
        });
    }
    
    //播放胜利过场
    public static void PlayWin(Action action)
    {
        ResetGameObj();
        var ani = ResourceMgr.CreateObj("StoryPageAni", canvas.transform);
        ani.GetComponent<StoryPageAni>().PlayAni("he", () =>
        {
            ResetGame();
            action.Invoke();
        });
    }
}