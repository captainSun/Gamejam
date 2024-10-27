using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryPageAni : MonoBehaviour
{
    public Image _image;
    public CanvasGroup _cg;
    public Button _button;
    public Dictionary<string, string[]> storyTexDic = new Dictionary<string, string[]>();
    public Dictionary<string, string> bubbleTexDic = new Dictionary<string, string>();
    private int curIndex = 0;
    private string curKey = "";
    private Action _endAction;
    private string[] pathList;
    public GameObject UIPrefab;
    public Transform parentTrans;
    private GameObject be_1;
    void Awake()
    {
        storyTexDic.Add("bridge", new string[]{"bridge"});
        storyTexDic.Add("be", new string[]{"be_1"});
        storyTexDic.Add("be_time_end", new string[]{"be_1"});
        storyTexDic.Add("be_life_end", new string[]{"be_1"});
        storyTexDic.Add("he", new string[]{"he_1","he_2"});
        storyTexDic.Add("fall", new string[]{"fall"});
        
        bubbleTexDic.Add("be", "走错路啦，这里是车站，被车撞倒啦！！");
        bubbleTexDic.Add("be_time_end", "完啦，没时间了，约会要迟到啦！！");
        bubbleTexDic.Add("be_life_end", "摔得好痛，走不了路了！！");
    }

    void Start()
    {
        _cg.alpha = 1;
        _button.gameObject.SetActive(false);
        _button.onClick.AddListener(() =>
        {
            PlayAni();
        }); 
    }

    public void PlayAni(string key = null, Action action = null)
    {
        if (key != null)
        {
            storyTexDic.TryGetValue(key, out pathList);
            curKey = key;
            _endAction = action;
        }

        _button.gameObject.SetActive(false);
        if (curIndex == pathList.Length)
        {
            //播放完毕
            
            var seq = DOTween.Sequence();
            seq.Append(_cg.DOFade(0, 0.2f));
            seq.AppendCallback(() =>
            {
                Destroy(gameObject);
            });
            _endAction.Invoke();
        }
        else
        {
            //下一张
            var path = pathList[curIndex];
            _image.sprite = ResourceMgr.LoadResAsset<Sprite>("ani/" + path, AssetsEnum.Texture);
            _image.SetNativeSize();
            curIndex++;
            if (path == "he_1")
            {
                _image.transform.localScale = Vector3.one * 0.75f;
            }
            else
            {
                _image.transform.localScale = Vector3.one;

                string notice;
                if (bubbleTexDic.TryGetValue(curKey, out notice))
                {
                    //显示气泡
                    this.be_1 = Instantiate(UIPrefab, transform);
                    var b = this.be_1.transform.Find("Text").GetComponent<TextMeshProUGUI>();
                    var cg = be_1.GetComponent<CanvasGroup>();
                    b.text = notice;
                    cg.alpha = 0;
                    cg.DOFade(1f, 0.5f);
                }
               
            }
            _image.color = Color.clear;
            var seq = DOTween.Sequence();
            seq.Append(_image.DOColor(Color.white, 0.5f));
            seq.AppendInterval(1);
            seq.AppendCallback(() =>
            {
                _button.gameObject.SetActive(true);
            });
        }
        
        
       
    }


}
