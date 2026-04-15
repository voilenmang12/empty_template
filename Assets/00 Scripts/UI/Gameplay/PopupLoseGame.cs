using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopupLoseGame : UIBase
{
    public Button btnRestart, btnHome, btnCheck, btnContinue;
    public CanvasGroup canvasGroup;
    private void Start()
    {
        btnRestart.onClick.AddListener(OnClickRestart);
        btnHome.onClick.AddListener(OnClickHome);
        btnCheck.onClick.AddListener(OnClickCheck);
        btnContinue.onClick.AddListener(OnClickContinue);
        btnContinue.gameObject.SetActive(false);
    }

    public override void Show()
    {
        AudioManager.Instance.PlaySfx(ESfx.LoseSfx);
        base.Show();
    }

    void OnClickHome()
    {
        GameManager.Instance.GoSceneHome();
    }

    void OnClickRestart()
    {
        GameManager.Instance.PlayGame(GameManager.Instance.GameType);
    }

    void OnClickCheck()
    {
        btnContinue.gameObject.SetActive(true);
        canvasGroup.DOKill();
        canvasGroup.DOFade(0, 0.5f).SetUpdate(true);
    }

    void OnClickContinue()
    {
        btnContinue.gameObject.SetActive(false);
        canvasGroup.DOKill();
        canvasGroup.DOFade(1, 0.5f).SetUpdate(true);
    }
}
