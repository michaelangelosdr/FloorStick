using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Game.Utils;

public class GameScreenController : MonoBehaviour
{
    [SerializeField]
    private GameObject ScreenOriginalParent;

    [SerializeField]
    private GameObject ScreenGameobject;

    [SerializeField]
    private CanvasGroup ScreenCanvasGroup;

    [SerializeField]
    private Transform Parent;

    [SerializeField]
    private Button ScreenCallerButton;

    [SerializeField]
    private Button ScreenCloserButton;

    [SerializeField]
    private GameScreenConfig gameScreenConfig;

    public virtual void Initialize()
    {
        ResetScreen();
        ScreenCallerButton.onClick.AddListener(() => { OnScreenCallerButtonClicked(true); });
        ScreenCloserButton.onClick.AddListener(() => { OnScreenCallerButtonClicked(false); });

        if (ScreenOriginalParent == null)
        {
            ScreenOriginalParent = this.gameObject;
        }
    }

    private void OnScreenCallerButtonClicked(bool isShow)
    {
        ShowScreen(isShow).Forget();
    }

    private async UniTask ShowScreen(bool isShow)
    {
        if (isShow)
        {
            ScreenGameobject.SetActive(true);
            ScreenGameobject.transform.SetParent(Parent);
            ScreenCanvasGroup.alpha = 0;
            ScreenCanvasGroup.interactable = true;
            ScreenCanvasGroup.blocksRaycasts = true;
            await CanvasGroupFadeUtil.FadeIn(ScreenCanvasGroup, gameScreenConfig.fadeDuration);
        }
        else
        {
            ScreenCanvasGroup.alpha = 1;
            ScreenCanvasGroup.interactable = false;
            ScreenCanvasGroup.blocksRaycasts = true;
            await CanvasGroupFadeUtil.FadeOut(ScreenCanvasGroup, gameScreenConfig.fadeDuration);
            ScreenCanvasGroup.blocksRaycasts = false;
            ScreenGameobject.transform.SetParent(ScreenOriginalParent.transform);
            ScreenGameobject.SetActive(false);
        }
    }

    internal void ResetScreen()
    {
        ScreenCanvasGroup.alpha = 0;
        ScreenCanvasGroup.interactable = false;
        ScreenCanvasGroup.blocksRaycasts = false;
        ScreenGameobject.transform.SetParent(this.gameObject.transform);
        ScreenGameobject.SetActive(false);
    }
}
