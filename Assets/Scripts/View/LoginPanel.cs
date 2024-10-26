using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class LoginPanel : MonoBehaviour
{
    private Button btn;
    public GameObject introduce;
    public Button introBtn;
    public TextMeshProUGUI BtnText;
    void Start()
    {
        btn = transform.Find("Button").GetComponent<Button>();
        btn.onClick.AddListener(StartGame);
        introBtn.onClick.AddListener(ActiveIntroduce);

    }

    void StartGame()
    {
        Destroy(gameObject);
        GameMgr.PlayOpen();
    }
    void ActiveIntroduce()
    {
        introduce.SetActive(true);
        BtnText.text = "关闭";
        introBtn.onClick.AddListener(()=>{
            BtnText.text = "联系我们";
            introduce.SetActive(false);
            introBtn.onClick.AddListener(ActiveIntroduce);
        });

    }
}
