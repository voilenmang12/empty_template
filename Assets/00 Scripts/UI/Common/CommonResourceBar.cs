using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CommonResourceBar : MonoBehaviour
{
    public Image iconResource;
    public TextMeshProUGUI txtValue;
    public float timeDelay, timeAnim;
    bool firstTime;
    long currentValue;
    Tween animTween;
    Coroutine delayCoroutine;
    public ECommonResource resourceType;

    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
            btn.onClick.AddListener(OnClick);
        TigerForge.EventManager.StartListening(Constant.ON_PLAYER_RESOURCE_UPDATE, UpdateResource);
        iconResource.sprite = DataSystem.Instance.dataSprites.dicCommomSprites[resourceType];
        UpdateResource();
    }

    private void OnDisable()
    {
        if (animTween != null)
            animTween.Kill();
        StopAllCoroutines();
    }

    private void OnEnable()
    {
        long nextValue = PlayerResource.Instance.GetCommonResource(resourceType);
        currentValue = nextValue;
        txtValue.text = currentValue.ToString();
    }

    void UpdateResource()
    {
        long nextValue = PlayerResource.Instance.GetCommonResource(resourceType);
        if (!firstTime || !gameObject.activeInHierarchy)
        {
            currentValue = nextValue;
            txtValue.text = currentValue.ToString();
            firstTime = true;
        }
        else
        {
            if (delayCoroutine != null)
                StopCoroutine(delayCoroutine);
            delayCoroutine = StartCoroutine(IEDelayAnim(nextValue));
        }
    }

    IEnumerator IEDelayAnim(long nextValue)
    {
        if (nextValue > currentValue)
            yield return new WaitForSecondsRealtime(timeDelay);
        if (animTween != null)
            animTween.Kill();
        animTween = DOTween.To(() => currentValue, x => currentValue = x, nextValue, timeAnim)
            .SetUpdate(true).OnUpdate(() => { txtValue.text = currentValue.ToString(); })
            .OnComplete(() =>
            {
                currentValue = nextValue;
                txtValue.text = currentValue.ToString();
            });
    }

    void OnClick()
    {
        AudioManager.Instance.PlaySfx(ESfx.ButtonSfx);
        if (GameplayManager.Instance == null)
        {
            UIManager.Instance.uIHome.GoShopPanel();
        }
        else
        {
            if (GameplayManager.Instance.GameEnded)
                return;
            UIManager.Instance.ShowPopupShopCoin();
        }
    }
}