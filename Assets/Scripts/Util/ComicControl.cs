using NodeCanvas.Tasks.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComicControl : MonoBehaviour
{
    public List<Sprite> comicPages;
    public Image displayImg;
    private int currentPageIndex = 0;
    private CanvasGroup canvasG;
    // Start is called before the first frame update
    void Start()
    {
        canvasG = GetComponent<CanvasGroup>();
        canvasG.alpha = 1.0f;
        //displayImg = this.GetComponent<Image>();
    }

    public void OnPageClicked()
    {
        if (currentPageIndex < comicPages.Count - 1)
        {
            StartCoroutine(SwitchPage());
        }
        else {
            SceneManager.LoadScene("Game");
        }

    }

    private IEnumerator SwitchPage()
    {
        yield return CartoonFadeOut();
        currentPageIndex++;
        displayImg.sprite = comicPages[currentPageIndex];
        yield return CartoonFadeIn();
    }

    private IEnumerator CartoonFadeOut()
    {
        while (canvasG.alpha > 0)
        {
            canvasG.alpha -= Time.deltaTime;
            yield return null;
        }
    }    
    private IEnumerator CartoonFadeIn()
    {
        while (canvasG.alpha < 1)
        {
            canvasG.alpha += Time.deltaTime;
            yield return null;
        }
    }

}