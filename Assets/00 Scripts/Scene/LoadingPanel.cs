using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;
using TMPro;
public class LoadingPanel : Singleton<LoadingPanel>
{
    public Image splashImage;
    public Image loadingFill;
    public Canvas loadingCanvas;
    public RectTransform loadingObj;
    public TextMeshProUGUI txtLoading;
    public bool Playing { get; private set; }
    string subfix = ".";
    string prefix = "Loading";
    public GameObject loadingWait;
    private void Start()
    {
        StartCoroutine(IETextLoading());
    }

    IEnumerator IETextLoading()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            VisualTextLoading();
        }
    }
    public void VisualTextLoading()
    {
        subfix += ".";
        if (subfix.Length > 3)
            subfix = ".";
        txtLoading.text = prefix + subfix;
    }
    public void ShowTextLoading(string text)
    {
        return;
        prefix = text;
        VisualTextLoading();
    }
    public void ShowLoadingWait(bool wait)
    {
        loadingWait.SetActive(wait);
    }
    public void StartLoading()
    {
        splashImage.gameObject.SetActive(true);
        loadingFill.DOKill();
        loadingFill.fillAmount = (0);
        loadingObj.gameObject.SetActive(true);
        gameObject.SetActive(true);
        loadingFill.DOFillAmount(0.7f, 1f).SetUpdate(true);
    }
    public IEnumerator EndLoading()
    {
        loadingFill.DOKill();
        bool fill = true;
        loadingFill.DOFillAmount(1,1f).SetUpdate(true).OnComplete(() =>
        {
            fill = false;
        });
        yield return new WaitUntil(() => !fill);
        loadingObj.gameObject.SetActive(false);
        // txtLoading.gameObject.SetActive(false);
    }
    public void ShowWaitNetworkPanel()
    {

    }
    public void HideWaitNetworkPanel()
    {

    }
}
