using UnityEngine;
using UnityEngine.UI;

public class HandleEnterIcon : MonoBehaviour
{
    public GameObject infoImage;  // 拖入悬浮时要显示的 Image
    //public string infoText;       // 需要显示的信息文本

    void Start()
    {
        // 确保提示信息图片默认隐藏
        infoImage.SetActive(false);
    }

    // 鼠标进入时触发
    public void OnMouseEnter()
    {
        infoImage.SetActive(true);
        //infoImage.GetComponentInChildren<Text>().text = infoText; // 显示文本内容
    }

    // 鼠标移出时触发
    public void OnMouseExit()
    {
        infoImage.SetActive(false);
    }
}