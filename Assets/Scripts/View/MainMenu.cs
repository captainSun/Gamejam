using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI remainTime;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        remainTime = transform.Find("RemainTime").GetComponent<TextMeshProUGUI>();
        scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }

    public void StartLevel()
    {
    }

}
