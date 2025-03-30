using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class FadeScreenController : MonoBehaviour
{
    [SerializeField]
    private Image fadeScreen;

    public bool isFadeAnimating;

    CancellationTokenSource cts = new CancellationTokenSource();

    public void SetFadeScreenOpacity(float opacity)
    {
        fadeScreen.color = new Color(0, 0, 0, opacity);
    }

    public async UniTask StartFadeIn(float fadeSpeed = 1f)
    {
        if (isFadeAnimating)
        {
            cts.Cancel();
        }

        isFadeAnimating = true;
        await FadeAsync(0f, 1f, fadeSpeed);
    }

    public async UniTask StartFadeOut(float fadeSpeed = 1f)
    {
        if (isFadeAnimating)
        {
            cts.Cancel();
        }

        isFadeAnimating = true;
        await FadeAsync(1f, 0f, fadeSpeed);
    }

    private async UniTask FadeAsync(float startAlpha, float endAlpha, float fadeDuration)
    {
        CancellationToken token = cts.Token;
        if (fadeScreen == null)
        {
            Debug.LogError("Image to fade is not assigned.");
            return;
        }

        float elapsedTime = 0f;
        Color color = fadeScreen.color;
        while (elapsedTime < fadeDuration)
        {
            token.ThrowIfCancellationRequested();

            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            color.a = alpha;
            fadeScreen.color = color;
            await UniTask.Yield();
        }

        color.a = endAlpha;
        fadeScreen.color = color;
        isFadeAnimating = false;
    }

    public void CancelFade()
    {
        cts.Cancel();
        fadeScreen.color = new Color(0, 0, 0, 0);
    }

    public void Destroy()
    {
        cts.Cancel();
        GameObject.Destroy(this.gameObject);
    }
}
